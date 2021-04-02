import React from 'react';
import {BrowserRouter, Route, Switch} from "react-router-dom";
import {Helmet} from 'react-helmet';
import Login from "./login/login";
import Header from "./header/header";

class App extends React.Component{

    render(){
        return (
            <main>
                <Helmet>
                    <style>{`body { background-color: #0a014f;
                        background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24'%3E%3Cg fill='%23fae8eb' fill-opacity='0.13'%3E%3Cpolygon fill-rule='evenodd' points='8 4 12 6 8 8 6 12 4 8 0 6 4 4 6 0 8 4'/%3E%3C/g%3E%3C/svg%3E"); }`}</style>
                </Helmet>

                <BrowserRouter>
                    <Switch>
                        <Route exact path='/login'>
                            <Login/>
                        </Route>

                        <Route exact path='/'>
                            Hola
                        </Route>

                        <Route exact path='/test'>
                            <Header/>
                        </Route>
                    </Switch>
                </BrowserRouter>
            </main>
        )
    }

}

export default App;
