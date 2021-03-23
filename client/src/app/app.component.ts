import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
  //Fetch the data and display
  //implements OnInit - to use the life cycle event after the constructor
export class AppComponent implements OnInit {
  title = 'client';
  //no typescript safety
  //THis is what's populated from the getUsers functions
  users : any;

  //depenedency injection
  //Inside the constructor is where we decalre what we want to inject
  //Life cycle after the constructor is known as the initialization
  constructor(private http: HttpClient) {}
  ngOnInit() {
    this.getUsers();
  }

  //Response we get back from our API
  getUsers()
  {
    this.http.get('https://localhost:5001/api/users').subscribe(response => {
      this.users = response;
    }, error => {
      console.log(error);
    })
  }
}



