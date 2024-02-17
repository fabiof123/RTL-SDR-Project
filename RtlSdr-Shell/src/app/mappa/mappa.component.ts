import { ChangeDetectorRef, Component, ElementRef, NgZone, OnInit, ViewChild } from '@angular/core';
import { MapMarker, GoogleMapsModule } from '@angular/google-maps'
import { AppComponent } from '../app.component';
import { FormsModule } from '@angular/forms';
import {MatIconModule} from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import {MatButtonModule} from '@angular/material/button';
import {MatSelectModule} from '@angular/material/select';
import {MatCardModule} from '@angular/material/card';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {ThemePalette} from '@angular/material/core';
import {
  MatDialog,
  MAT_DIALOG_DATA,
  MatDialogRef,
  MatDialogTitle,
  MatDialogContent,
  MatDialogActions,
  MatDialogClose,
} from '@angular/material/dialog';
import { GraficoComponent } from '../grafico/grafico.component';
import { CommonModule } from '@angular/common';
import { RtlsdrService } from '../servizi/rtlsdr.service';

interface MarkerProperties{
  latitudine: number;
  longitudine: number;
};

interface FiltriProperties{
  frequenzaIniziale: number;
  frequenzaFinale: number;
};

interface LettureNoDett {
  id: number;
  data: string;
  frequenzaIniziale: number;
  frequenzaFinale: number;
  latitudine: number;
  longitudine: number;
};

export interface DialogData {
  id: number;
};

@Component({
    selector: 'app-mappa',
    standalone: true,
    templateUrl: './mappa.component.html',
    styleUrl: './mappa.component.css',
    imports: [
        MatInputModule,
        FormsModule,
        GoogleMapsModule,
        MapMarker,
        AppComponent,
        MatIconModule,
        MatDividerModule,
        MatButtonModule,
        MatSelectModule,
        MatCardModule,
        MatFormFieldModule,
        GraficoComponent,
        CommonModule
    ]
})
export class MappaComponent implements OnInit {

  constructor(private cdr: ChangeDetectorRef, public dialog: MatDialog, private zone: NgZone, private rtlsdrService: RtlsdrService) {}
  
  Selezionato = false;
  MostraFiltri = false;
  Latitudine = 0;
  Longitudine = 0;
  display: any;
  center: google.maps.LatLngLiteral = {
      lat: 46.0715725,
      lng: 11.1081598
  };
  zoom = 9;

  markers: MarkerProperties[] | undefined;
  filtriFrequenze: FiltriProperties[] | undefined;

  letture: LettureNoDett[] = [];

  markerOptions: google.maps.MarkerOptions = {draggable: false};

  markerList: google.maps.Marker[] = [];
  self = this;
  prova: any | undefined;
  markerInizializzati = false;

  ngOnInit(): void {
    this.rtlsdrService.getPosizioni().subscribe((data: any) => {
      console.log(data);
      this.markers = data.result;
      console.log(this.markers);
      this.markerInizializzati = true;
    });
  }

  handleMapInitializad(map: google.maps.Map){
    this.markers?.forEach((marker: MarkerProperties) => {
      var mark = new google.maps.Marker({
        position: {lat: marker.latitudine, lng: marker.longitudine},
        map,
        icon: "../../assets/images/MarkerRosso-50.png",
      });
      this.markerList.push(mark);

      /* Punto selezionato */
      google.maps.event.addListener(mark, 'click', (function(marker, mark, markers, self) {
        return function() {
          markers.forEach(function (m){
            m.setIcon("../../assets/images/MarkerRosso-50.png");
          })
          mark.setIcon("../../assets/images/MarkerBlu-50.png");
          self.Selezionato = true;
          self.Latitudine = marker.latitudine;
          self.Longitudine = marker.longitudine;
          self.MostraFiltri = false;
          self.cdr.detectChanges();
          self.rtlsdrService.getListaLetturePosizione(self.Latitudine.toString(), self.Longitudine.toString()).subscribe((data: any) => {
            self.letture = data.result;
            console.log(self.letture);
            self.cdr.detectChanges();
          })
        };
      })(marker, mark, this.markerList, this.self));
    });
  }

  /* NON c'è un punto selezionato */
  mapClick(event: google.maps.MapMouseEvent){
    this.markerList.forEach(function (m){
      m.setIcon("../../assets/images/MarkerRosso-50.png");
    })
    this.Selezionato = false;
    this.MostraFiltri = false;
    this.cdr.detectChanges();
  }

  mostraFiltri(event: MouseEvent){
    this.MostraFiltri = !this.MostraFiltri;
    if(this.MostraFiltri == true){
      this.rtlsdrService.getFiltriFrequenze(this.Latitudine.toString(), this.Longitudine.toString()).subscribe((data: any) => {
        console.log(data);
        this.filtriFrequenze = data.result;
        console.log(this.filtriFrequenze);
        this.cdr.detectChanges();
      });
    }
    this.cdr.detectChanges();
  }

  vedi(identificatore: number){
    this.zone.run(() => {
      const dialogRef = this.dialog.open(GraficoComponent, {
        data: {id: identificatore},
      });
  
      dialogRef.afterClosed().subscribe(result => {
        console.log('Grafico Chiuso');
      });
    });
  }

  filtroSelezione(event: any){
    console.log(event.value);
  }

  cambioFiltro(selected: string){
    if(selected == "tutte"){
      this.rtlsdrService.getListaLetturePosizione(this.Latitudine.toString(), this.Longitudine.toString()).subscribe((data: any) => {
        this.letture = data.result;
        console.log(this.letture);
        this.cdr.detectChanges();
      });
    }else{
      let sel: string[] = selected.split('/');
      this.rtlsdrService.getListaLetturePosizioneFrequenze(this.Latitudine.toString(), this.Longitudine.toString(), sel[0], sel[1]).subscribe((data: any) => {
        this.letture = data.result;
        console.log(this.letture);
        this.cdr.detectChanges();
      });
    }
  }

}
