import { Injectable } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RtlsdrService {
  baseurl: string = 'http://fabiof123-001-site2.jtempurl.com/api/services/app/Lettura/';

  constructor(private http: HttpClient) { }

  getPosizioni(){
    //return this.http.get(this.baseurl+'GetListaPosizioni', { responseType: 'json', observe: 'response' })
    return this.http.get(this.baseurl+'GetListaPosizioni', { responseType: 'json' })
  }

  getFiltriFrequenze(lat: string, lon: string){
    //return this.http.get(this.baseurl+'GetListaPosizioni', { responseType: 'json', observe: 'response' })
    return this.http.get(this.baseurl+'GetListaFrequenzeNellaPosizione?Latitudine='+lat+'&Longitudine='+lon, { responseType: 'json' })
  }

  getListaLetture(){
    return this.http.get(this.baseurl+'GetListaLetture', { responseType: 'json' })
  }

  getListaLetturePosizione(lat: string, lon: string){
    return this.http.get(this.baseurl+'GetListaLetture?Latitudine='+lat+'&Longitudine='+lon, { responseType: 'json' })
  }

  getListaLetturePosizioneFrequenze(lat: string, lon: string, frI: string, frF: string){
    return this.http.get(this.baseurl+'GetListaLetture?Latitudine='+lat+'&Longitudine='+lon+'&FrequenzaIniziale='+frI+'&FrequenzaFinale='+frF, { responseType: 'json' })
  }

  getLetturaId(id: string){
    return this.http.get(this.baseurl+'GetLettura?id='+id, { responseType: 'json' })
  }

}
