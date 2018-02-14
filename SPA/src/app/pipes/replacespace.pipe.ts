import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'replacespace'
})
export class ReplacespacePipe implements PipeTransform {

  transform(value: any, args?: any): any {
    if (/\s/.test(value)) {
    let newValue = value.replace(/\s/g, '-');
    return `${newValue}`;
    }else{
      return value;
    }
  }

}



// import { Pipe, PipeTransform } from '@angular/core';
// @Pipe({name: 'replaceLineBreaks'})
// export class ReplaceLineBreaks implements PipeTransform {
//   transform(value: string): string {
//     let newValue = value.replace(/\n/g, '<br/>');
//     return `${newValue}`;
//   }
// }