import {ChangeDetectorRef, Component, NgZone, OnInit} from '@angular/core';
import {MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { RtlsdrService } from '../servizi/rtlsdr.service';
import { GraficoComponent } from '../grafico/grafico.component';

export interface Letture {
  id: number;
  data: string;
  frequenzaIniziale: number;
  frequenzaFinale: number;
  latitudine: number;
  longitudine: number;
}

export interface DialogData {
  id: number;
};

@Component({
  selector: 'app-tabella',
  standalone: true,
  imports: [
    MatTableModule,
    MatButtonModule],
  templateUrl: './tabella.component.html',
  styleUrl: './tabella.component.css'
})
export class TabellaComponent implements OnInit {
  displayedColumns: string[] = ['id', 'data', 'latitudine', 'longitudine', 'frequenzaIniziale', 'frequenzaFinale', 'bottone'];
  letture: Letture[] = [];

  constructor(private cdr: ChangeDetectorRef, public dialog: MatDialog, private zone: NgZone, private rtlsdrService: RtlsdrService) {}
  
  
  ngOnInit(): void {
    this.rtlsdrService.getListaLetture().subscribe((data: any) => {
      this.letture = data.result;
      console.log(this.letture);
      this.cdr.detectChanges();
    });
  }

  vedi(id: number){
    console.log(id);
    this.zone.run(() => {
      const dialogRef = this.dialog.open(GraficoComponent, {
        data: {id: id},
      });
  
      dialogRef.afterClosed().subscribe(result => {
        console.log('Grafico Chiuso');
      });
    });
  }
}
