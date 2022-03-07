import { HttpClient } from '@angular/common/http';
import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
    form: FormGroup;

    constructor(private formBuiler: FormBuilder, private http: HttpClient) { }

    ngOnInit() {
        this.form = new FormGroup({
            'Username': new FormControl(null, Validators.required),
            'Password': new FormControl(null, Validators.required),
            'FirstName': new FormControl(null, Validators.required),
            'LastName': new FormControl(null, Validators.required),
            'Address': new FormControl(null, Validators.required),
            'PhoneNumber': new FormControl(null, Validators.required)
        });
    }

    onSubmit() {
        this.http.post("https://localhost:44300/user", this.form.value).subscribe(data => { }, error => console.log("Error: " + error.Message));
    }
}

//interface User {
//    username: string,
//    password: string,
//    firstname: string,
//    lastname: string,
//    address: string,
//    phonenumber: number
//}
