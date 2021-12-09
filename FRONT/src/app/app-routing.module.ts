import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InicioComponent } from './inicio/inicio.component';
import { LandingComponent } from './landing/landing.component';
import { LoginComponent } from './login/login.component';
import { MenuComponent } from './menu/menu.component';
import { RegistroComponent } from './registro/registro.component';
import { TransferenciaComponent } from './transferencia/transferencia.component';
import { VerDetallesComponent } from './ver-detalles/ver-detalles.component';

import { NosotrosComponent } from './nosotros/nosotros.component';
import { ContactoComponent } from './contacto/contacto.component';

import { TransferenciaOtraCuentaComponent } from './transferencia-otra-cuenta/transferencia-otra-cuenta.component';
import { TransferenciaContactoComponent } from './transferencia-contacto/transferencia-contacto.component';
import { TransferenciaCuentaPropiaComponent } from './transferencia-cuenta-propia/transferencia-cuenta-propia.component';
import { MovPesosComponent } from './mov-pesos/mov-pesos.component';
import { DatosUsuarioComponent } from './datos-usuario/datos-usuario.component';
import { PageNotComponent } from './page-not/page-not.component';
import { CotizacionesCryptoComponent } from './cotizaciones-crypto/cotizaciones-crypto.component'

const routes: Routes = [
  {path: '', component:LandingComponent},
  {path: 'registro', component:RegistroComponent},
  {path: 'login', component:LoginComponent},
  
  {path: 'contacto', component:ContactoComponent},
  {path: 'nosotros', component:NosotrosComponent},
 
  {path: '404', component:PageNotComponent},
  {path: 'paginaprincipal', component:MenuComponent,children:[
    {path: 'transferencia', component:TransferenciaComponent},
    {path: 'detalles', component:VerDetallesComponent},
    {path: 'inicio', component:InicioComponent},
    {path: 'transferenciaotra',component:TransferenciaOtraCuentaComponent},
    {path:'transferenciacontacto',component:TransferenciaContactoComponent},
    {path:'transferenciacuentapropia',component:TransferenciaCuentaPropiaComponent},
    {path: 'mov-pesos', component:MovPesosComponent},
    {path: 'mod-usuario', component:DatosUsuarioComponent},
    {path: 'cot-crypto', component:CotizacionesCryptoComponent}
  ]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
