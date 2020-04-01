import { AbstractControl } from '@angular/forms';

export function MedicineValidator(control: AbstractControl) {
  if (!control.value || !control.value.genericCode) {
    return { validMedicine: true };
  }
  return null;
}
