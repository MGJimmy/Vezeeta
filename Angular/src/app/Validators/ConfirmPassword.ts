import { AbstractControl, ValidationErrors } from "@angular/forms";

export function ConfirmPasswordValidator(control:AbstractControl){
    const password=control.get('PasswordHash')
    const confirmPassword=control.get('confirmPassword')
    
 
    if(password?.pristine || confirmPassword?.pristine)
    {
        return null;
    }
    else
    {
     return password && confirmPassword && password.value !== confirmPassword.value 
     ?{'misMatch':true}
     : null;
    }
}
