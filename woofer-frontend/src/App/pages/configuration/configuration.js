import React from 'react';
import {Modal} from "@material-ui/core";
import backIcon from "../../../img/icon/arrow-left.svg";
import {currentIP, lsUserKey} from "../../constants/keys";
import {
    devRootURL,
    followedCommunityApiURLs,
    postApiURLs,
    userApiURLs,
    userPictureApiURLs
} from "../../constants/api-url";
import picIcon from "../../../img/icon/photograph.svg";
import firebase from "firebase";
import {randomString} from "../../shared_components/randomString";
import {Post} from "../../../models/post";

class Configuration extends React.Component{

    state = {
        passwordModal: false,
        userDataModal: false,
        deleteUserModal: false,
        newPassword: '',
        confirmPassword: '',
        pictureFile: null,
        name: '',
        id: 0,
        lastName: '',
        email: '',
        picture: '',
        nickname: ''
    }

    constructor() {
        super();

        this.handleSubmitPassword = this.handleSubmitPassword.bind(this);
        this.handleSubmitUserData = this.handleSubmitUserData.bind(this);
        this.handleSubmitDeleteUser = this.handleSubmitDeleteUser.bind(this);

        const acc = JSON.parse(localStorage.getItem(lsUserKey));

        fetch(`${devRootURL}${userApiURLs.getById(acc.id)}`,{
            method: 'GET'
        })
            .then(response => response.json())
            .then((json)=>{
                this.setState({
                    nickname: json.nickName,
                    name: json.name,
                    lastName: json.lastName,
                    id: json.id,
                    email: json.email,

                });

            })
            .catch(err => console.log(err));

        fetch(`${devRootURL}${userPictureApiURLs.byUserId(acc.id)}`,{
            method: 'GET'
        })
            .then(response => response.json())
            .then((json)=>{

                this.setState({

                    picture: json.serverPath

                });

                console.log(json);


            })
            .catch(err => console.log(err));
    }

    handleSubmitDeleteUser(){

        if (this.state.newPassword === 'CONFIRMAR'){

            fetch(`${devRootURL}${userApiURLs.delete(JSON.parse(localStorage.getItem(lsUserKey)).id)}`,{
                method: 'GET'
            })
                .then(response => {if( response.status === 200){

                    localStorage.removeItem(lsUserKey);
                    window.location.href = `http://${currentIP}/login`;

                }})
                .then((json)=>{
                })
                .catch(err => console.log(err));

        }

    }

    handleSubmitUserData(){

        const file = this.state.pictureFile;

        if (file !== null) {

            const storageRef = firebase.storage().ref(`/${file.name}__${randomString(12)}`);

            const task = storageRef.put(file);

            task.on('state_changed',
                snapshot => {
                },
                error => {
                    console.log(error)
                },
                () => {

                    const putBody = JSON.stringify({
                        id: this.state.id,
                        nickName: this.state.nickname,
                        password: null,
                        serverPath: storageRef.name,
                        email: this.state.email,
                        name: this.state.name,
                        lastName: this.state.lastName
                    });

                    fetch(`${devRootURL}${userApiURLs.edit}`,{
                        method: 'PUT',
                        headers: {'Content-type': 'application/json;charset=UTF-8'},
                        body: putBody
                    })
                        .then(response => {

                            if (response.status === 200){

                                this.setState({userDataModal: false});

                            }

                        })

                        .catch(err => console.log(err));

                }
            );
        }
        else{

            const putBody = JSON.stringify({
                id: this.state.id,
                nickName: this.state.nickname,
                password: null,
                serverPath: null,
                email: this.state.email,
                name: this.state.name,
                lastName: this.state.lastName
            });

            fetch(`${devRootURL}${userApiURLs.edit}`,{
                method: 'PUT',
                headers: {'Content-type': 'application/json;charset=UTF-8'},
                body: putBody
            })
                .then(response => {

                    if (response.status === 200){

                        this.setState({userDataModal: false});

                    }

                })

                .catch(err => console.log(err));

        }

    }

    handleSubmitPassword() {

        const putBody = JSON.stringify({
            id: this.state.id,
            nickName: this.state.nickname,
            password: this.state.newPassword,
            serverPath: null,
            email: this.state.email,
            name: this.state.name,
            lastName: this.state.lastName
        });

            if (this.state.newPassword === this.state.confirmPassword){

                fetch(`${devRootURL}${userApiURLs.edit}`,{
                    method: 'PUT',
                    headers: {'Content-type': 'application/json;charset=UTF-8'},
                    body: putBody
                })
                    .then(response => {

                        if (response.status === 200){

                            this.setState({passwordModal: false});

                        }

                    })
                    .then(()=>{})
                    .catch(err => console.log(err));

            }

    }

    render(){

        return <div className={"flex flex-col items-center w-full mt-32 xl:ml-72"}>

                        <div onClick={() => {

                            this.setState({passwordModal: true});

                        }}
                            className={`text-white bg-dark border border-white px-4 py-2 mt-5 
                        hover:bg-white hover:text-dark hover:border-primary cursor-pointer rounded`}>
                            Cambiar contraseña
                        </div>

                        <div  onClick={() => {

                            this.setState({userDataModal: true});

                        }}
                            className={`text-white bg-dark border border-white px-4 py-2 mt-5 
                        hover:bg-white hover:text-dark hover:border-primary cursor-pointer rounded`}>
                            Cambiar datos de usuario
                        </div>

                        <div  onClick={() => {

                            this.setState({deleteUserModal: true});

                        }}
                              className={`text-white bg-dark border border-white px-4 py-2 mt-5 
                                    hover:bg-white hover:text-dark hover:border-primary cursor-pointer rounded`}>
                            Borrar cuenta
                        </div>


            <Modal open={this.state.passwordModal}>
                {<section className={"flex flex-col bg-dark w-full h-full md:w-5/6 md:h-3/4 m-auto md:mt-40 text-white py-4 shadow-innerW"}>
                    <div className={"flex flex-row justify-between items-center mt-6 md:mt-2"}>
                        <button onClick={() => {
                            this.setState({passwordModal: false});
                        }} className={"transform scale-150 ml-8 text-white "}>
                            <img src={backIcon} alt="Back"/>
                        </button>
                        <button onClick={this.handleSubmitPassword}
                                className={"rounded-full w-32 h-8 font-bold text-dark bg-light hover:bg-primary ml-40 mr-4 md:mr-12"}>Confirmar</button>
                    </div>

                    <div className={"flex flex-col mb-2 mx-auto mt-4"}>
                        <label className={"text-white"} htmlFor="newPassword">New password</label>
                        <input onChange={($e) => {
                            this.setState({newPassword: $e.target.value});
                        }}
                               className={"h-7 px-5 py-2 rounded bg-dark border border-white text-white"} type="password" id={"newPassword"}/>
                    </div>

                    <div className={"flex flex-col mb-2 mx-auto mt-2"}>
                        <label className={"text-white"} htmlFor="confirmPassword">Confirm new password</label>
                        <input onChange={($e) => {
                            this.setState({confirmPassword: $e.target.value});
                        }}
                               className={"h-7 px-5 py-2 rounded bg-dark border border-white text-white"} type="password" id={"confirmPassword"}/>
                    </div>
                </section>}
            </Modal>

            <Modal open={this.state.userDataModal}>
                {<section className={"flex flex-col bg-dark w-full h-full md:w-5/6 md:h-3/4 m-auto md:mt-40 text-white py-4 shadow-innerW"}>
                    <div className={"flex flex-row justify-between items-center mt-6 md:mt-2"}>
                        <button onClick={() => {
                            this.setState({userDataModal: false});
                        }} className={"transform scale-150 ml-8 text-white "}>
                            <img src={backIcon} alt="Back"/>
                        </button>
                        <button onClick={this.handleSubmitUserData}
                                className={"rounded-full w-32 h-8 font-bold text-dark bg-light hover:bg-primary ml-40 mr-4 md:mr-12"}>Confirmar</button>
                    </div>

                    <div className={"flex flex-col mb-2 mx-auto mt-4"}>
                        <label className={"text-white"} htmlFor="name">Name</label>
                        <input onChange={($e) => {
                            this.setState({name: $e.target.value});
                        }}
                               className={"h-7 px-5 py-2 rounded bg-dark border border-white text-white"} type="text" id={"name"}/>
                    </div>

                    <div className={"flex flex-col mb-2 mx-auto mt-2"}>
                        <label className={"text-white"} htmlFor="lastName">Last name</label>
                        <input onChange={($e) => {
                            this.setState({lastName: $e.target.value});
                        }}
                               className={"h-7 px-5 py-2 rounded bg-dark border border-white text-white"} type="text" id={"lastName"}/>
                    </div>

                    <div className={"flex flex-col mb-2 mx-auto mt-2"}>
                        <label className={"text-white"} htmlFor="picture">Avatar</label>
                        <label>
                            <img className={"transform scale-150"} src={picIcon} alt="Imagen"/>
                            <input onChange={($e) => {this.setState({ pictureFile:  $e.target.files[0] });}}
                                   id={"picture"} className={"hidden"} type="file" accept="image/png, image/jpeg"/>
                        </label>
                    </div>
                </section>}
            </Modal>


            <Modal open={this.state.deleteUserModal}>
                {<section className={"flex flex-col bg-dark w-full h-full md:w-5/6 md:h-3/4 m-auto md:mt-40 text-white py-4 shadow-innerW"}>
                    <div className={"flex flex-row justify-between items-center mt-6 md:mt-2"}>
                        <button onClick={() => {
                            this.setState({deleteUserModal: false});
                        }} className={"transform scale-150 ml-8 text-white "}>
                            <img src={backIcon} alt="Back"/>
                        </button>
                        <button onClick={this.handleSubmitDeleteUser}
                                className={"rounded-full w-32 h-8 font-bold text-dark bg-light hover:bg-primary ml-40 mr-4 md:mr-12"}>Confirmar</button>
                    </div>

                    <div className={"flex flex-col mb-2 mx-auto mt-4"}>
                        <label className={"text-white px-5 mb-2"} htmlFor="newPassword">Si desea borrar la cuenta, escriba CONFIRMAR y de click en el botón de Confirmar</label>
                        <input onChange={($e) => {
                            this.setState({newPassword: $e.target.value});
                        }}
                               className={"h-7 px-5 py-2 rounded bg-dark border border-white text-white"} type="text" id={"newPassword"}/>
                    </div>

                </section>}
            </Modal>
                </div>;

    }

}

export default Configuration;
