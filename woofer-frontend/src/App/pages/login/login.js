import React from 'react';
import logoIcon from "../../../logo.svg";
import {ErrorLog} from "../../ErrorLog/errorLog";
import {devRootURL, userApiURLs} from "../../constants/api-url";
import {currentIP, lsUserKey} from "../../constants/keys";

class Login extends React.Component{

    constructor() {
        super();

        this.handleNicknameChange = this.handleNicknameChange.bind(this);
        this.handlePasswordChange = this.handlePasswordChange.bind(this);
        this.render = this.render.bind(this);
        this.submit = this.submit.bind(this);

    }

    state = { errorLog: []};

    submit(){

        const errorList = [];
        let errorFlag = false;

        if (this.state.nickname === '' || this.state.nickname === undefined || this.state.nickname === null){

            errorList.push('No se ha introducido el Nickname.');
            errorFlag = true;

        }

        if (this.state.password === '' || this.state.password === undefined || this.state.password === null){

            errorList.push('No se ha introducido la Contraseña.');
            errorFlag = true;

        }

        this.setState({errorLog: [...errorList]});

        if(errorFlag){
            return;
        }

        const postBody = JSON.stringify({

            nickName: this.state.nickname,
            password: this.state.password

        });

        fetch(`${devRootURL}${userApiURLs.login}`,{
            method: 'POST',
            headers: {'Content-type': 'application/json;charset=UTF-8'},
            body: postBody
        })
            .then(response => response.json())
            .then((json)=>{
                localStorage.setItem(lsUserKey, JSON.stringify({acc: this.state.nickname, id: json.id}));
                window.location.href =`http://${currentIP}/`;
            })
            .catch(err => console.log(err));



    }

    handleNicknameChange($e) {
        this.setState({nickname: $e.target.value});
    };
    handlePasswordChange($e){
        this.setState({password: $e.target.value});
    };

    render() {
        return (

            <article className={"bg-dark border rounded md:w-2/3 xl:w-1/3 mx-auto transform translate-y-16 md:translate-y-32"}>

                <figure className={"mx-auto w-2/3"}>
                    <img src={logoIcon} alt="woofer"/>
                </figure>

                <ErrorLog messages={[...this.state.errorLog]}/>

                <div className={" py-5 mx-auto"}>
                    <div className={"flex flex-col items-center w-2/5 mx-auto"}>
                        <a href={`http://${currentIP}/register`} className={"text-white underline self-end mr-5"}>Registrarse</a>
                        <div className={"flex flex-col mb-2"}>
                            <label className={"text-white"} htmlFor="nickname">Nickname</label>
                            <input onChange={this.handleNicknameChange}
                                   className={"h-7 px-5 py-2 rounded bg-dark border border-white text-white"} type="text" id={"nickname"}/>
                        </div>

                        <div className={"flex flex-col mb-2"}>
                            <label className={"text-white"} htmlFor="password">Contraseña</label>
                            <input onChange={this.handlePasswordChange}
                                   className={"h-7 px-5 py-2 rounded bg-dark border border-white text-white"} type="password" id={"password"}/>
                        </div>

                        <button onClick={this.submit}
                                className={"mt-5 rounded-full w-64 h-12 font-bold text-dark bg-primary mx-auto mt-4 hover:bg-light"}>Entrar</button>
                    </div>

                </div>
            </article>
        );
    }

}

export default Login;
