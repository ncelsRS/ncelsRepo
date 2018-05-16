import { Component, ViewEncapsulation } from '@angular/core';
import "leaflet-map";
import "style-loader!leaflet/dist/leaflet.css";

@Component({
  selector: 'app-leaflet-maps',
  templateUrl: './leaflet-maps.component.html',
  encapsulation: ViewEncapsulation.None
})
export class LeafletMapsComponent {

   ngAfterViewInit() {

    let el = document.getElementById("leaflet-map");

    L.Icon.Default.imagePath = 'assets/img/vendor/leaflet';
    var map = L.map(el).setView([51.505, -0.09], 13);
    L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
      attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(map);

    L.marker([51.5, -0.09]).addTo(map)
      .bindPopup('A pretty CSS3 popup.<br> Easily customizable.')
      .openPopup();
  }

}
