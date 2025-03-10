import { Component, OnInit } from '@angular/core';
import { CheckoutService, } from '../../paypal.service';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-paypal-button',
  templateUrl: './paypal-button.component.html',
  styleUrl: './paypal-button.component.scss'
})
export class PaypalButtonComponent implements OnInit {
  customerId!: number
  sumForPay!: number

  constructor(private _checkoutService: CheckoutService, private _router: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this._router.params.subscribe((param) => {
      this.customerId = +param['userId'];
      this.sumForPay = +param['cost']
    })
  }
  createOrder() {
    const send = {
      customerId: this.customerId,
      sumForPay: this.sumForPay
    }
    this._checkoutService.createOrder(send).subscribe({
      next: (res) => {
        window.location.href = res.approveLink;

      },
      error: (err) => {
        console.error('Failed to create order', err);
      }
    }
    )
  }
}




