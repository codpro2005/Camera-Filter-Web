import { ValidatorFn } from '@angular/forms';

export class Error {
    name: string;
    message: string;
    validator: ValidatorFn;

    public constructor(name: string, message: string, validator: ValidatorFn) {
        this.name = name;
        this.message = message;
        this.validator = validator;
    }
}