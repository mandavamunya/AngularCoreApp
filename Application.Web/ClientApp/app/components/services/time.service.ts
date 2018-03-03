import { Injectable, Inject } from '@angular/core';

@Injectable()
export class Time {
    constructor() { }


    public monthIndex(date: string): number
    {
        return this.datetime(date).getMonth();
    }  
    public month(date: string): string
    {
        return this.monthToString(this.datetime(date).getMonth());
    }

    public datemonth(date: string): string
    {
      return this.datetime(date).getDate() + " " + this.monthToString(this.datetime(date).getMonth());
    }

    public datemonthyear(date: string): string
    {
      return this.datetime(date).getDate() + " " + this.monthToString(this.datetime(date).getMonth()) + " " + this.datetime(date).getFullYear();
    }
  
    public datetime(datetime: string): Date
    {
        return new Date(datetime);
    }

    public utcMonth(datetime: string)
    {
        return this.datetime(datetime).getUTCMonth();
    }
    
    private monthToString(index: number): string
    {
      var months = new Array("January", "February", "March", "April", "May", "June", "July", "August", "September","October", "November", "December");
      return (months[index]);
    }   
}