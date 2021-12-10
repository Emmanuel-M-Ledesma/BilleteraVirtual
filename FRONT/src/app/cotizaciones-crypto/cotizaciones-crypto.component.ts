import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

interface Coin{
  id:string;
  name: string;
  symbol:string;
  image: string;
  current_price: number;
  price_change_percentage_24h: number;
  total_volume: number;
}

@Component({
  selector: 'app-cotizaciones-crypto',
  templateUrl: './cotizaciones-crypto.component.html',
  styleUrls: ['./cotizaciones-crypto.component.css']
})
export class CotizacionesCryptoComponent implements OnInit {

  coins: Coin[]= []
  filteredCoins:Coin[]= []
  titles: string[]=[
    '#',
    'Cripto',
    'Precio',
    'Cambio de precio',
    'Volumen 24 horas',
  ]
  searchText='';

  constructor(private http: HttpClient){}

  searchCoin(){

    console.log( this.searchText)

    this.filteredCoins = this.coins.filter(coin=>
      coin.name.toLowerCase().includes(this.searchText.toLocaleLowerCase()) ||
      coin.symbol.toLowerCase().includes(this.searchText.toLocaleLowerCase())
      );
  }

  ngOnInit(): void {
    this.http.get<Coin[]>('https://api.coingecko.com/api/v3/coins/markets?vs_currency=ars&order=market_cap_desc&per_page=100&page=1&sparkline=false')
    .subscribe(
      (res) => {
        console.log(res);
        this.coins= res;
        this.filteredCoins= res;
      },
      (err)=>console.log(err)
    );  
  }

}

