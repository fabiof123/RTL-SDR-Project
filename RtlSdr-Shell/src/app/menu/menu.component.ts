import { AfterContentChecked, AfterContentInit, AfterViewChecked, AfterViewInit, Component, DoCheck, OnDestroy, OnInit } from '@angular/core';

import {MatTabsModule} from '@angular/material/tabs';
import { MappaComponent } from "../mappa/mappa.component";
import { TabellaComponent } from "../tabella/tabella.component";

@Component({
    selector: 'app-menu',
    standalone: true,
    templateUrl: './menu.component.html',
    styleUrl: './menu.component.css',
    imports: [MatTabsModule, MappaComponent, TabellaComponent]
})
export class MenuComponent implements OnInit, AfterContentChecked, AfterContentInit, AfterViewChecked, AfterViewInit, DoCheck, OnDestroy{

  constructor(){
    // console.log("Costruttore");
  }

  ngOnInit(): void {
    // console.log("ngOnInit");
  }

  ngAfterContentChecked(): void {
    // console.log("ngAfterContentChecked");
  }

  ngAfterContentInit(): void {
    // console.log("ngAfterContentInit");
  }

  ngAfterViewChecked(): void {
    // console.log("ngAfterViewChecked");
  }

  ngAfterViewInit(): void {
    // console.log("ngAfterViewInit");
  }

  ngDoCheck(): void {
    // console.log("ngDoCheck");
  }

  ngOnDestroy(): void {
    // console.log("ngOnDestroy");
  }

}
