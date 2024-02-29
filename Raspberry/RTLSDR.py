#!/usr/bin/python
# -*- coding:utf-8 -*-
import RPi.GPIO as GPIO
import serial
import time
from pylab import *
import numpy as np
from rtlsdr import RtlSdr 
import multiprocessing
import json
import requests
import random

# Variabili, pipe, serial
listaFrequenzeScansione = [99, 101, 103, 105, 107, 109, 111, 113, 115] #lista in MHz
tempoCiclo = 60 * 10 #10 minuti
timeout_lettura = 60 * 2 #2 minuti
conn1, conn2 = multiprocessing.Pipe()
ser = serial.Serial('/dev/ttyS0',115200)
ser.flushInput()
power_key = 4
rec_buff = ''
rec_buff2 = ''
time_count = 0

#Classi
class PotenzaSegnale:
	def __init__(self, frequenza: float, valore: float):
		self.frequenza = frequenza
		self.valore = valore

class Lettura:
	def __init__(self, frequenzaIniziale: float, frequenzaFinale: float, latitudine: float, longitudine: float, potenzeSegnali: list):
		self.frequenzaIniziale = frequenzaIniziale
		self.frequenzaFinale = frequenzaFinale
		self.latitudine = latitudine
		self.longitudine = longitudine
		self.potenzeSegnali = potenzeSegnali

#Funzioni
def send_at(command,back,timeout):
	rec_buff = ''
	ser.write((command+'\r\n').encode())
	time.sleep(timeout)
	if ser.inWaiting():
		time.sleep(0.01 )
		rec_buff = ser.read(ser.inWaiting())
	if rec_buff != '':
		if back not in rec_buff.decode():
			print(command + ' ERROR')
			print(command + ' back:\t' + rec_buff.decode())
			return 'errore'
		else:
			return rec_buff.decode()
	else:
		print('GPS is not ready')
		return 'errore'

def get_gps_position():
	answer = 0
	print('Start GPS session...')
	rec_buff = ''
	send_at('AT+CGNSPWR=1','OK',1)
	time.sleep(2)
	answer = send_at('AT+CGNSINF','+CGNSINF: ',1)
	if 'errore' != answer:
		if ',,,,,,' in rec_buff:
			print('GPS is not ready')
			rec_null = False
			time.sleep(1)
	else:
		print('error %d'%answer)
		send_at('AT+CGPS=0','OK',1)
		return 'Lettura Fallita'
	return answer


def power_on(power_key):
	print('SIM7600X is starting:')
	GPIO.setmode(GPIO.BCM)
	GPIO.setwarnings(False)
	GPIO.setup(power_key,GPIO.OUT)
	time.sleep(0.1)
	GPIO.output(power_key,GPIO.HIGH)
	time.sleep(2)
	GPIO.output(power_key,GPIO.LOW)
	time.sleep(2)
	ser.flushInput()
	print('SIM7600X is ready')

def power_down(power_key):
	print('SIM7600X is loging off')
	GPIO.output(power_key,GPIO.HIGH)
	time.sleep(3)
	GPIO.output(power_key,GPIO.LOW)
	time.sleep(2)

def readSamples(centralFreq):
	sdr = RtlSdr()
	sdr.sample_rate = 1.95e6 
	sdr.center_freq = centralFreq
	#sdr.freq_correction = 60
	sdr.gain = 'auto'
	let = sdr.read_samples(256*256)
	sdr.close()
	datiFrequenze = psd(let, NFFT=256, Fs=sdr.sample_rate/1e6, Fc=sdr.center_freq/1e6)
	#
	#xlabel('Frequency (MHz)')
	#ylabel('Relative power (dB)')
	#show()
	#
	temp = []
	temp.append((np.log10(datiFrequenze[0])) * 10)
	temp.append(datiFrequenze[1])
	conn2.send(temp)
	return

#Main
try:
	while True:
		listaPosizioni = []
		while True:
			power_on(power_key)
			letturaPosizione = get_gps_position()
			power_down(power_key)
			listaPosizioni = letturaPosizione.split(',')
			#print(listaPosizioni)
			if ((len(listaPosizioni)>5) and (listaPosizioni[3] != '') and (listaPosizioni[4] != '')):
				break
		valoreLettura = (random.choice(listaFrequenzeScansione)) * 1e6
		p = multiprocessing.Process(target=readSamples, args={valoreLettura})
		p.start()
		p.join(timeout_lettura)
		lettura = conn1.recv()
		latitudine = float(listaPosizioni[3])
		longitudine = float(listaPosizioni[4])
		listaPotenzaSegnali = []
		cont = 0
		while cont < lettura[0].size:
			listaPotenzaSegnali.append(PotenzaSegnale(lettura[1][cont], lettura[0][cont]))
			cont = cont+1
		listaJson = json.dumps([ob.__dict__ for ob in listaPotenzaSegnali])
		obj = json.loads(listaJson)
		letturaInvio = Lettura((valoreLettura/1e6)-1, (valoreLettura/1e6)+1, latitudine, longitudine, obj)
		letturaJson = json.dumps(letturaInvio.__dict__)
		print(letturaJson)
		response = requests.post('http://fabiof123-001-site2.jtempurl.com/api/services/app/Lettura/CreateLettura', json = json.loads(letturaJson))
		print(response)
		time.sleep(tempoCiclo)
except:
	if ser != None:
		ser.close()
	power_down(power_key)
	GPIO.cleanup()
if ser != None:
		ser.close()
		GPIO.cleanup()	
