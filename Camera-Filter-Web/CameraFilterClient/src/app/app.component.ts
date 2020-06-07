import { Component, OnInit } from '@angular/core';
import { CameraFilterApiService } from './Services/CameraFilterApiService/camera-filter-api.service';
import { Named } from './models/named';
import { FormBuilder, FormGroup, FormControl, FormArray, Validators } from '@angular/forms';
import { BehaviorSubject, Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  public get invalidlySubmitted(): Observable<boolean> {
    return this.invalidSubmitBehaviourSubject.asObservable();
  }

  public get imageFilterName(): string {
    return this.formData.name;
  }

  private get filterNameFilterParametersPairs(): Named<Named<string>[]>[] {
    return this.formData.value;
  }

  public get filterNames(): string[] {
    return this.filterNameFilterParametersPairs.map(filter => filter.name);
  }

  constructor(private fb: FormBuilder, private cameraFilterApiService: CameraFilterApiService) { }

  public static vowels = ['a', 'e', 'i', 'o', 'u'];

  public title = 'Camera Filter';
  private filterSelectValue = '0';
  public filterSelectKey = 'filterSelect';
  public parametersKey = 'parameters';
  public mediaUploaderKey = 'mediaUploader';
  public mediaValidationMessage = AppComponent.generateRequiredMessage('Media');
  public form: FormGroup;
  public formData: Named<Named<Named<string>[]>[]>;
  public parametersData: Named<string>[];
  private invalidSubmitBehaviourSubject = new BehaviorSubject(false);
  private mediaBase64: string;
  public filteredMediaBase64Raw: string;

  public static generateRequiredMessage(name: string): string {
    return `${name} required`;
  }

  private getFilterParameters(filterIndex: number): Named<string>[] {
    return this.filterNameFilterParametersPairs[filterIndex].value;
  }

  private onFilterSelectChange(filterIndex) {
    const parametersData = this.getFilterParameters(filterIndex);
    this.parametersData = parametersData;
    this.form.setControl(this.parametersKey, parametersData
    .reduce((formArray: FormArray, parameterData: Named<string>) => {
      formArray.push(this.fb.control(['', Validators.required]));
      return formArray;
    }, new FormArray([])));
  }

  ngOnInit() {
    this.onFilterSelectChange = this.onFilterSelectChange.bind(this);
    this.form = this.fb.group({
      [this.filterSelectKey]: [this.filterSelectValue],
      [this.parametersKey]: this.fb.array([]),
      [this.mediaUploaderKey]: ['', Validators.required],
    });
    this.form.controls[this.filterSelectKey].valueChanges.subscribe(this.onFilterSelectChange);
    this.cameraFilterApiService.getFormData()
      .subscribe(form => {
        this.formData = form;
        this.onFilterSelectChange(this.filterSelectValue);
      });
  }

  public handleMediaUpload(event) {
    const files: FileList = event.target.files;
    if (!files.length) { return; }
    const file = files[0];
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => this.mediaBase64 = reader.result as string;
  }

  public onSubmit() {
    if (this.form.invalid) {
      this.invalidSubmitBehaviourSubject.next(true);
      return;
    }
    const values = this.form.value;
    const filterIndex = values[this.filterSelectKey];
    const parameters = values[this.parametersKey];
    const mediaBase64 = this.mediaBase64;
    this.cameraFilterApiService.postFilteredData(filterIndex, parameters, mediaBase64).subscribe(filteredMediaBase64RawRefenrece => this.filteredMediaBase64Raw = filteredMediaBase64RawRefenrece.value);
  }
}
