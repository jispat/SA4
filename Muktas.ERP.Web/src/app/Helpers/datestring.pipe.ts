// import {Pipe, PipeTransform} from '@angular/core';
// import {DatePipe} from '@angular/common';

// @Pipe({ name: 'dateString' })

// export class DateString implements PipeTransform {

//     constructor(public datepipe:  DatePipe){}

//     transform(value: Date, args: any[]): string {
//         //var parsedDate = Date.parse(value.toString());
//         //console.log("pipe");
//         //alert(value);

//         if (value === undefined || value === null) {
//             return undefined;
//         }
//         else {
//             //var parsedDate = Date.parse(value.toString());
//             debugger;
//             //let d = new Date(value);
//             //let v = moment(d).format('dd-MM-yyyy');
//             let v = this.datepipe.transform(value, 'dd-MM-yyyy');
//             return v;
//             //return new DatePipe("").transform(new Date(parsedDate), args[0]);
//             //return parsedDate.toString();
//         }
//     }
// }
