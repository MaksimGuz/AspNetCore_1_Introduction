import React, { Component } from 'react';
//import unregister from '../registerServiceWorker';

export class Home extends Component {
    displayName = Home.name;

    prepareForAspnetcore() {
        //console.log("begin prepare");
        //unregister();
        //console.log("end prepare");
    }

    render() {
        return (
            <div>
                <h1>Home Page</h1>
                <h1>Welcome to container on linux world!</h1>
                <p>Welcome to the single-page application, built with:</p>
                <ul>
                    <li><a href='https://get.asp.net/'>ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>
                    <li><a href='https://facebook.github.io/react/'>React</a> for client-side code</li>
                    <li><a href='http://getbootstrap.com/'>Bootstrap</a> for layout and styling</li>
                </ul>
                <br />
                <a href="/Home" onClick={() => { this.prepareForAspnetcore(); return true; }} >Перейти к ASP.NET Core MVC</a>
            </div>
        );
    }
}
