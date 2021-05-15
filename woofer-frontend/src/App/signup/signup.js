import React from 'react';
import logoIcon from '../../logo.svg';
import { devRootURL, userApiURLs } from '../constants/api-url';
import {ErrorLog } from '../ErrorLog/errorLog';


class Signup extends React.Component {

    constructor() {
        super();
        this.register = this.register.bind(this);
        this.handleNicknameChange = this.handleNicknameChange.bind(this);
        this.handleEmailChange = this.handleEmailChange.bind(this);
        this.handlePasswordChange = this.handlePasswordChange.bind(this);
        this.handleNameChange = this.handleNameChange.bind(this);
        this.handleLastNameChange = this.handleLastNameChange.bind(this);
    }

 state = {

        errorLog: ''

 };

    handleNicknameChange($e) {
    this.setState({nickname: $e.target.value});
    };
    handleEmailChange($e) {
    this.setState({email: $e.target.value});
    };
    handlePasswordChange($e){
    this.setState({password: $e.target.value});
    };
    handleNameChange($e){
    this.setState({name: $e.target.value});
    };
    handleLastNameChange($e){
    this.setState({lastName: $e.target.value});
    };

    register(){

        const errorList = [];
        let errorFlag = false;

        if (this.state.nickname === '' || this.state.nickname === undefined || this.state.nickname === null){

            errorList.push('No se ha introducido el Nickname.');
            errorFlag = true;

        }

        if (this.state.email === '' || this.state.email === undefined || this.state.email === null){

            errorList.push('No se ha introducido el Correo.');
            errorFlag = true;
        }

        if (this.state.password === '' || this.state.password === undefined || this.state.password === null){

            errorList.push('No se ha introducido la Contraseña.');
            errorFlag = true;
        }

        if (this.state.name === '' || this.state.name === undefined || this.state.name === null){

            errorList.push('No se ha introducido el Nombre.');
            errorFlag = true;
        }

        if (this.state.lastName === '' || this.state.lastName === undefined || this.state.lastName === null){

            errorList.push('No se han introducido los Apellidos.');
            errorFlag = true;
        }

        this.setState({errorLog: [...errorList]});

        if (errorFlag){
            return;
        }

        const postBody = JSON.stringify({

            nickname: this.state.nickname,
            email: this.state.email,
            password: this.state.password,
            name: this.state.name,
            lastName: this.state.lastName,
            picture: ''
        });

        fetch(`${devRootURL}${userApiURLs.signUp}`,{
            method: 'POST',
            headers: {'Content-type': 'application/json;charset=UTF-8'},
            body: postBody
        })
            .then(response => response.json())
            .then((res)=>{
                localStorage.setItem('woofer-user-ac', JSON.stringify({acc: this.state.nickname, id: res}));
                window.location.href ='http://localhost:3000/';
            })
            .catch(err => console.log(err));
    };

    render(){
        return (

            <article className={"bg-dark border rounded md:w-2/3 xl:w-1/3 mx-auto transform translate-y-16 md:translate-y-32"}>

                <figure className={"mx-auto w-2/3"}>
                    <img src={logoIcon} alt="woofer"/>
                </figure>

                <ErrorLog messages={[...this.state.errorLog]}/>

                <div className={"flex flex-col items-center py-5 mx-auto"}>

                    <div className={"flex flex-col w-1/3 mb-2"}>
                        <label className={"text-white"} htmlFor="nickname">Nickname</label>
                        <input onChange={this.handleNicknameChange}
                            className={"h-7 px-5 py-2 rounded"} type="text" id={"nickname"}/>
                    </div>

                    <div className={"flex flex-col w-1/3 mb-2"}>
                        <label className={"text-white"} htmlFor="email">Correo</label>
                        <input onChange={this.handleEmailChange}
                            className={"h-7 px-5 py-2 rounded"} type="email" id={"email"}/>
                    </div>

                    <div className={"flex flex-col w-1/3 mb-2"}>
                        <label className={"text-white"} htmlFor="password">Contraseña</label>
                        <input onChange={this.handlePasswordChange}
                            className={"h-7 px-5 py-2 rounded"} type="password" id={"password"}/>
                    </div>

                    <div className={"flex flex-col w-1/3 mb-2"}>
                        <label className={"text-white"} htmlFor="name">Nombre</label>
                        <input onChange={this.handleNameChange}
                            className={"h-7 px-5 py-2 rounded"} type="text" id={"name"}/>
                    </div>

                    <div className={"flex flex-col w-1/3"}>
                        <label className={"text-white"} htmlFor="lastName">Apellidos</label>
                        <input onChange={this.handleLastNameChange}
                            className={"h-7 px-5 py-2 rounded"} type="text" id={"lastName"}/>
                    </div>

                    <button onClick={this.register}
                        className={"mt-5 rounded-full w-64 h-12 font-bold text-dark bg-primary mx-auto mt-4 hover:bg-light"}>Registrar</button>

                </div>
            </article>
        );
    }

}

export default Signup;
