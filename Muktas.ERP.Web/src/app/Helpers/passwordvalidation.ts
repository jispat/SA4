import {AbstractControl} from '@angular/forms';
export class PasswordValidation {

    static MatchPassword(AC: AbstractControl) {
       let password = AC.get('Password').value; // to get value in input tag
       let confirmPassword = AC.get('ConfirmPassword').value; // to get value in input tag
        if(password != confirmPassword) {
            console.log('false');
            AC.get('ConfirmPassword').setErrors( {MatchPassword: true} )
        } else {
            console.log('true');
            return null
        }
    }

    static MatchPasswordResetPassword(AC: AbstractControl) {
       let password = AC.get('NewPassword').value; // to get value in input tag
       let confirmPassword = AC.get('ConfirmPassword').value; // to get value in input tag
        if(password != confirmPassword) {
            console.log('false');
            AC.get('ConfirmPassword').setErrors( {MatchPassword: true} )
        } else {
            console.log('true');
            return null
        }
    }

}
