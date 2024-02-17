import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MenuComponent } from "./menu/menu.component";

import {MatTabsModule} from '@angular/material/tabs';

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [RouterOutlet,
      MenuComponent,
      MatTabsModule,
    ]
})
export class AppComponent {
  title = 'RtlSdr-Shell';
}
