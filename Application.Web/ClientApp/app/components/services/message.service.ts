import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';

@Injectable()
export class MessageService {
  private message: string = <string>{};
  private success: boolean = <boolean>{};
  private info: boolean = <boolean>{};
  private warning: boolean = <boolean>{};
  private danger: boolean = <boolean>{};

  reset()
  {
    this.message = "";
    this.success = false;
    this.info = false;
    this.warning = false;
    this.danger = false; 
  }

  timeOut()
  {
    setTimeout(() => this.reset(), 2000);
  }

  setMessage(message: string,  alertType: string)
  {
    this.message = message;
    this.setAlertType(alertType);
    this.timeOut();
  }

  private setAlertType(alertType: string)
  {
    if (alertType == "success")
      this.success = true;
    else if (alertType == "info")
      this.info = true;
    else if (alertType == "warning")
      this.warning = true;
    else
      this.danger = true;
  }

}
