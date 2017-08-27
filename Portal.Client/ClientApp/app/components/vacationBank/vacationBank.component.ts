import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'vacationBank',
    templateUrl: './vacationBank.component.html'
})
export class VacationBankComponent {
    public employee: Employee;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/Employees/ca1b1787-2774-4fb2-9fa3-a7dc01677424').subscribe(result => {
            this.employee = result.json() as Employee;
        }, error => console.error(error));
    }
}

interface Employee {
    VacationHours: number;
}
