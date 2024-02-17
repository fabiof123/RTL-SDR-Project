import { ChangeDetectorRef, Component, Inject, OnInit, ViewChild } from '@angular/core';
import {
  MatDialog,
  MAT_DIALOG_DATA,
  MatDialogRef,
  MatDialogTitle,
  MatDialogContent,
  MatDialogActions,
  MatDialogClose,
} from '@angular/material/dialog';
import { MappaComponent } from '../mappa/mappa.component';
import { Chart, ChartConfiguration, ChartType } from 'chart.js';
import { BaseChartDirective, NgChartsModule } from 'ng2-charts';
import { RtlsdrService } from '../servizi/rtlsdr.service';

export interface DialogData {
  id: number;
};

interface PotenzaSegnale{
  id: number;
  frequenza: number;
  valore: number;
}

interface Lettura {
  id: number;
  data: string;
  frequenzaIniziale: number;
  frequenzaFinale: number;
  latitudine: number;
  longitudine: number;
  potenzaSegnali: PotenzaSegnale[];
};

@Component({
  selector: 'app-grafico',
  standalone: true,
  imports: [
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    MatDialogClose,
    NgChartsModule
  ],
  templateUrl: './grafico.component.html',
  styleUrl: './grafico.component.css'
})
export class GraficoComponent implements OnInit{
  /*@Input() data: any;*/

  constructor(private cdr: ChangeDetectorRef, public dialogRef: MatDialogRef<MappaComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData, private rtlsdrService: RtlsdrService){
      Chart.register();
  }

  @ViewChild(BaseChartDirective) chart?: BaseChartDirective;
  lettura: Lettura | undefined;

  public lineChartData: ChartConfiguration['data'] = {
    datasets: [
      {
        data: [],
        label: 'Potenza Segnale [DB]',
        yAxisID: 'y',
        backgroundColor: 'rgba(81,45,168 ,0.3)',
        borderColor: '#512DA8',
        pointBackgroundColor: 'rgba(148,159,177,1)',
        pointBorderColor: '#fff',
        pointHoverBackgroundColor: '#fff',
        pointHoverBorderColor: 'rgba(148,159,177,0.8)',
        fill: 'origin',
      },
    ],
    labels: [],
  };

  ngOnInit(): void {
    console.log("Componente GRAFICO ONINIT ESEGUITO");
    console.log(this.data.id);
    this.rtlsdrService.getLetturaId(this.data.id.toString()).subscribe((data: any) => {
      this.lettura = data.result;
      console.log(this.lettura);
      this.lettura?.potenzaSegnali.forEach(x => {
        this.lineChartData.datasets[0].data.push(x.valore);
        this.lineChartData.labels?.push(x.frequenza);
      })
      console.log(this.lineChartData);
      this.chart?.update();
      this.cdr.detectChanges();
    });
  }

  public lineChartOptions: ChartConfiguration['options'] = {
    elements: {
      line: {
        tension: 0.5,
      },
    },
    scales: {
      // We use this empty structure as a placeholder for dynamic theming.
      y: {
        position: 'left',
      },
    },

    plugins: {
      legend: { display: true },
    },
  };

  public lineChartType: ChartType = 'line';

  /*options!: EChartsOption;
    
  ngOnInit(): void {
    console.log("grafico");
    const xAxisData = [];
      const data1 = [];
      const data2 = [];

      for (let i = 0; i < 100; i++) {
        xAxisData.push('category' + i);
        data1.push((Math.sin(i / 5) * (i / 5 - 10) + i / 6) * 5);
        data2.push((Math.cos(i / 5) * (i / 5 - 10) + i / 6) * 5);
      }

      this.options = {
        legend: {
          data: ['bar', 'bar2'],
          align: 'left',
        },
        tooltip: {},
        xAxis: {
          data: xAxisData,
          silent: false,
          splitLine: {
            show: false,
          },
        },
        yAxis: {},
        series: [
          {
            name: 'bar',
            type: 'bar',
            data: data1,
            animationDelay: idx => idx * 10,
          },
          {
            name: 'bar2',
            type: 'bar',
            data: data2,
            animationDelay: idx => idx * 10 + 100,
          },
        ],
        animationEasing: 'elasticOut',
        animationDelayUpdate: idx => idx * 5,
    }
    /*console.log(this.data.id);
  }*/

  /*public lineChartData: ChartConfiguration['data'] = {
    datasets: [
      {
        data: [65, 59, 80, 81, 56, 55, 40],
        label: 'Series A',
        backgroundColor: 'rgba(148,159,177,0.2)',
        borderColor: 'rgba(148,159,177,1)',
        pointBackgroundColor: 'rgba(148,159,177,1)',
        pointBorderColor: '#fff',
        pointHoverBackgroundColor: '#fff',
        pointHoverBorderColor: 'rgba(148,159,177,0.8)',
        fill: 'origin',
      }
    ],
    labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
  };*/

  /*public lineChartOptions: ChartConfiguration['options'] = {
    elements: {
      line: {
        tension: 0.5,
      },
    },
    scales: {
      y: {
        position: 'left',
      },
      y1: {
        position: 'right',
        grid: {
          color: 'rgba(255,0,0,0.3)',
        },
        ticks: {
          color: 'red',
        },
      },
    },

    plugins: {
      legend: { display: true },
    },
  };

  /*public lineChartOptions: ChartOptions = {
    responsive: true,
  };

  public lineChartLegend = true;
  public lineChartType: ChartType = 'line';
  public lineChartPlugins = [];*/
}
