import { Component, OnInit } from '@angular/core';
import { PayPalService } from '../../paypal.service';

@Component({
  selector: 'app-paypal-button',
  templateUrl: './paypal-button.component.html',
  styleUrl: './paypal-button.component.scss'
})
export class PaypalButtonComponent implements OnInit{
    approvalUrl!: string;
    
   constructor(private _payPalService:PayPalService){}
 
  
    ngOnInit(): void {
    }
  
    createPayment(){
        this._payPalService.createPayment()
    }
}
