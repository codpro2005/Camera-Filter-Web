import { Error } from './../../models/error';
import { Observable, Subscription } from 'rxjs';
import { Component, OnInit, Input, forwardRef, OnDestroy } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NG_VALIDATORS, FormBuilder, AbstractControl, ValidationErrors, Validators } from '@angular/forms';
import { AppComponent } from 'src/app/app.component';
import { TypeEnum } from 'src/app/models/typeEnum';

@Component({
  selector: 'app-type-control',
  templateUrl: './type-control.component.html',
  styleUrls: ['./type-control.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => TypeControlComponent),
      multi: true
    },
    {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => TypeControlComponent),
      multi: true
    }
  ]
})
export class TypeControlComponent implements OnInit, ControlValueAccessor, OnDestroy {

  constructor(private fb: FormBuilder) { }

  @Input() public type: string;
  @Input() public label: string;
  @Input() public fireError: Observable<boolean>;

  public typeEnum = TypeEnum;
  public errors: Error[] = [];

  public typeControl = this.fb.control('', Validators.required);

  private subscriptions = new Subscription();

  private static generatePatternTypeMessage(name: string): string {
    return `value must match a${AppComponent.vowels.includes(name[0].toLowerCase()) ? 'n' : ''} ${name} type`;
  }

  ngOnInit() {
    let defaultValue: any;
    switch (this.type) {
      case TypeEnum.Int32:
        this.errors.push(new Error('required', AppComponent.generateRequiredMessage(this.type), Validators.required), new Error('pattern', TypeControlComponent.generatePatternTypeMessage(this.type), Validators.pattern('^\\d+$')));
        break;
      case TypeEnum.Double:
        this.errors.push(new Error('required', AppComponent.generateRequiredMessage(this.type), Validators.required), new Error('pattern', TypeControlComponent.generatePatternTypeMessage(this.type), Validators.pattern('^-?\\d+(\\.\\d*)?$')));
        break;
      case TypeEnum.Color:
        break;
      default:
        break;
    }
    this.typeControl = this.fb.control(defaultValue, this.errors.map(error => error.validator)); // bug where default value must be changed in order for the value not to be "[defaultValue, null]" of type array but rather "defaultValue" of type string.
  }

  public writeValue(obj: any): void {
    const value = obj ? obj[0] : obj;
    value && this.typeControl.setValue(value, { emitEvent: false });
  }

  public registerOnChange(fn: any): void {
    this.subscriptions.add(this.typeControl.valueChanges.subscribe(fn));
  }

  public registerOnTouched(fn: any): void { }

  public setDisabledState?(isDisabled: boolean): void {
    isDisabled ? this.typeControl.disable() : this.typeControl.enable();
  }

  public validate(c: AbstractControl): ValidationErrors | null {
    return this.typeControl.valid ? null : { invalidForm: { valid: false, message: 'form fields are invalid' } };
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }
}
