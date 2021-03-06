import React from 'react';
import {BrowserRouter, Route, Switch} from "react-router-dom";
import {Helmet} from 'react-helmet';
import Login from "./pages/login/login";
import Layout from "./layout/layout";
import Home from "./pages/home/home";
import Signup from "./pages/signup/signup";
import Profile from './pages/profile/profile';
import Configuration from "./pages/configuration/configuration";
import Communities from './pages/communities/communities';
import CommunityView from "./pages/community/communityView";

class ProfileContainer extends React.Component{

    state = {


    }

    constructor(props) {
        super(props);

    }

    render(){

        return  <Layout>
            <Profile nickname={this.props.match.params.nickname}/>
        </Layout>

    }

}

class CommunityContainer extends React.Component{

    state = {


    }

    constructor(props) {
        super(props);

    }

    render(){

        return  <Layout>
            <CommunityView name={this.props.match.params.name} id={this.props.match.params.id}/>
        </Layout>

    }

}

class App extends React.Component{

    constructor(){
        super();

    }

    render(){
        return (
            <main>
                <Helmet>
                    <style>{`body { background-color: #000a17;
                        background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24'%3E%3Cg fill='%23fae8eb' fill-opacity='0.13'%3E%3Cpolygon fill-rule='evenodd' points='8 4 12 6 8 8 6 12 4 8 0 6 4 4 6 0 8 4'/%3E%3C/g%3E%3C/svg%3E"); }`}</style>
                </Helmet>

                <BrowserRouter>
                    <Switch>

                        <Route exact path='/'>

                            <Layout>
                                <Home/>
                            </Layout>

                        </Route>

                        <Route exact path='/login'>

                                <Login/>

                        </Route>

                        <Route exact path='/register'>

                                <Signup/>

                        </Route>

                        <Route path='/profile/:nickname' component={ProfileContainer}>

                        </Route>

                        <Route exact path='/communities/'>

                            <Layout>
                                <Communities/>
                            </Layout>

                        </Route>

                        <Route path='/community/:name/:id' component={CommunityContainer}>
                        </Route>

                        <Route exact path='/configuration'>
                            <Layout>
                                <Configuration/>
                            </Layout>
                        </Route>

                    </Switch>
                </BrowserRouter>

            </main>
        )
    }

}

export default App;
