import React from "react";
import backIcon from "../../../../../img/icon/arrow-left.svg";
import picIcon from "../../../../../img/icon/photograph.svg";
import firebase from "firebase";
import {randomString} from "../../../../shared_components/randomString";
import {communityApiURLs, devRootURL, postApiURLs} from "../../../../constants/api-url";
import {lsUserKey} from "../../../../constants/keys";

class CreateCommunity extends React.Component{

    state = {

        name: '',
        description: '',
        pictureFile: null,
        color: 'blue'

    }

    constructor(props) {
        super(props);


        this.handleChangeColor = this.handleChangeColor.bind(this);
        this.handleChangePicture = this.handleChangePicture.bind(this);
        this.showColor = this.showColor.bind(this);
        this.handleCreateCommunity = this.handleCreateCommunity.bind(this);

    }

    showColor() {

        switch(this.state.color){

            case 'bg-blue-800':{

                return 'azul';
            }

            case 'bg-red-800':{

                return 'rojo';
            }

            case 'bg-green-800':{

                return 'verde';
            }

            case 'bg-gray-800':{

                return 'gris';
            }

            case 'bg-black':{

                return 'negro';
            }

            case 'bg-pink-800':{

                return 'rosa';
            }

            case 'bg-purple-800':{

                return 'morado';
            }

            case 'bg-indigo-800':{

                return 'indigo';
            }

            case 'bg-yellow-800':{

                return 'naranja';
            }

            default:{

                return 'azul';
            }

        }

    }

    handleCreateCommunity(){

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

                    const postBody = JSON.stringify({

                        name: this.state.name,
                        color: this.state.color,
                        description: this.state.description,
                        picture: storageRef.name,
                        ownerId: JSON.parse(localStorage.getItem(lsUserKey)).id
                    });
                    fetch(`${devRootURL}${communityApiURLs.create}`, {
                        method: 'POST',
                        headers: {'Content-type': 'application/json;charset=UTF-8'},
                        body: postBody
                    })
                        .then(response => {
                            if (response.status === 200) {

                                this.props.closeModal();

                            }
                        }).then(()=>{})
                        .catch(err => console.log(err));

                }
            );
        }



    }


    handleChangeColor($e){
        this.setState({color: $e.target.value})
    }

    handleChangePicture($e){
        this.setState({ pictureFile:  $e.target.files[0] });
    }

    render(){

        return <section className={"flex flex-col bg-dark w-full h-full md:w-5/6 md:h-3/4 m-auto md:mt-40 text-white py-4 shadow-innerW"}>

            <h3 className={"font-bold text-light text-xl text-center"}>Crear publicación</h3>
            <div className={"flex flex-row justify-between items-center mt-6 md:mt-2"}>
                <button onClick={()=>{this.props.closeModal()}} className={"transform scale-150 ml-8 text-white "}>
                    <img src={backIcon} alt="Back"/>
                </button>
                <button onClick={this.handleCreateCommunity}
                        className={"rounded-full w-32 h-8 font-bold text-dark bg-light hover:bg-primary ml-40 mr-4 md:mr-12"}>Crear</button>
            </div>

            <div className={"flex flex-col mb-2 mx-auto mt-4"}>
                <label className={"text-white"} htmlFor="name">Nombre de la comunidad</label>
                <input onChange={($e) => {
                    this.setState({name: $e.target.value});
                }}
                       className={"h-7 px-5 py-2 rounded bg-dark border border-white text-white"} type="text" id={"name"}/>
            </div>

            <div className={"flex flex-col mb-2 mx-auto mt-4"}>
                <label className={"text-white"} htmlFor="name">Descripción</label>
                <input onChange={($e) => {
                    this.setState({description: $e.target.value});
                }}
                       className={"h-7 px-5 py-2 rounded bg-dark border border-white text-white"} type="text" id={"description"}/>
            </div>

            <div className={"flex flex-col mb-2 mx-auto mt-4"}>
                <label className={"text-white"} htmlFor="name">{`Color: ${this.showColor()}`}</label>

                <div className={"w-full grid grid-cols-3 grid-rows-3 gap-x-12 gap-y-4 p-1.5"}>

                    <label className={"w-8 h-8 bg-blue-800 rounded-full cursor-pointer"}>
                        <input onChange={this.handleChangeColor}
                            type="radio" name={"color-picker"} className={"hidden"} value={"bg-blue-800"}/>
                    </label>

                    <label className={"w-8 h-8 bg-green-800 rounded-full cursor-pointer"}>
                        <input onChange={this.handleChangeColor}
                            type="radio" name={"color-picker"} className={"hidden"} value={"bg-green-800"}/>
                    </label>

                    <label className={"w-8 h-8 bg-red-800 rounded-full cursor-pointer"}>
                        <input onChange={this.handleChangeColor}
                            type="radio" name={"color-picker"} className={"hidden"} value={"bg-red-800"}/>
                    </label>

                    <label className={"w-8 h-8 bg-purple-800 rounded-full cursor-pointer"}>
                        <input onChange={this.handleChangeColor}
                            type="radio" name={"color-picker"} className={"hidden"} value={"bg-purple-800"}/>
                    </label>

                    <label className={"w-8 h-8 bg-indigo-800 rounded-full cursor-pointer"}>
                        <input onChange={this.handleChangeColor}
                            type="radio" name={"color-picker"} className={"hidden"} value={"bg-indigo-800"}/>
                    </label>

                    <label className={"w-8 h-8 bg-pink-800 rounded-full cursor-pointer"}>
                        <input onChange={this.handleChangeColor}
                            type="radio" name={"color-picker"} className={"hidden"} value={"bg-pink-800"}/>
                    </label>

                    <label className={"w-8 h-8 bg-gray-800 rounded-full cursor-pointer"}>
                        <input onChange={this.handleChangeColor}
                            type="radio" name={"color-picker"} className={"hidden"} value={"bg-gray-800"}/>
                    </label>

                    <label className={"w-8 h-8 bg-yellow-800 rounded-full cursor-pointer"}>
                        <input onChange={this.handleChangeColor}
                            type="radio" name={"color-picker"} className={"hidden"} value={"bg-yellow-800"}/>
                    </label>

                    <label className={"w-8 h-8 bg-black rounded-full cursor-pointer"}>
                        <input onChange={this.handleChangeColor}
                            type="radio" name={"color-picker"} className={"hidden"} value={"bg-black"}/>
                    </label>
                </div>

            </div>

            <div className={"flex flex-col mb-2 mx-auto mt-2"}>
                <label className={"text-white"} htmlFor="picture">Avatar</label>
                <label>
                    <img className={"transform scale-150"} src={picIcon} alt="Imagen"/>
                    <input onChange={($e) => {this.setState({ pictureFile:  $e.target.files[0] });}}
                           id={"picture"} className={"hidden"} type="file" accept="image/png, image/jpeg"/>
                </label>
            </div>




        </section>

    }

}


export default CreateCommunity;
