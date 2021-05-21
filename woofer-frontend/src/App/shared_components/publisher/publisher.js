import React from 'react';
import {Modal} from "@material-ui/core";
import backIcon from '../../../img/icon/arrow-left.svg';
import picIcon from '../../../img/icon/photograph.svg';
import {devRootURL, followedCommunityApiURLs, postApiURLs, userApiURLs} from "../../constants/api-url";
import {firebaseConfig, lsUserKey} from "../../constants/keys";
import {Community} from "../../../models/community";
import firebase from 'firebase';
import {randomString} from "../randomString";

class Publisher extends React.Component{

    state = {
        openModal: false,
        communities: [],
        pictureFile: null
    }

    constructor(props) {
        super(props);


        this.toggleOpenModal = this.toggleOpenModal.bind(this);
        this.modalWindow = this.modalWindow.bind(this);
        this.publish = this.publish.bind(this);
        this.handleContentChange = this.handleContentChange.bind(this);
        this.handleCommunityChange = this.handleCommunityChange.bind(this);
        this.handlePictureChange = this.handlePictureChange.bind(this);

        const userId = JSON.parse(localStorage.getItem(lsUserKey)).id;

        fetch(`${devRootURL}${followedCommunityApiURLs.getByFollower(userId)}`,{
            method: 'GET'
        })
            .then(response => response.json())
            .then((json)=>{

                const followedCommunities = json.map((com) => {

                        return new Community({id: com.id, name: com.name,
                                            color: com.color, description: com.description,
                                            creationDate: com.creationDate, authorId: com.authorId,
                                            picture: com.picture
                                            });

                });

                this.setState({communities: [...followedCommunities]});
                this.setState({communityId: followedCommunities[0].id});

            })
            .catch(err => console.log(err));

        fetch(`${devRootURL}${userApiURLs.getById(userId)}`,{
            method: 'GET'
        })
            .then(response => response.json())
            .then((json)=>{

                this.setState({picture: json.picture, name: json.name})
            })
            .catch(err => console.log(err));
    }

    handleContentChange($e) {
        this.setState({content: $e.target.value});
    };

    handleCommunityChange($e) {
        this.setState({communityId: $e.target.value});
    };

    handlePictureChange($e) {
        this.setState({ pictureFile:  $e.target.files[0] });
    };

    uploadImage() {

        const storageRef = firebase.storage().ref(`/${this.state.pictureFile.name}${randomString(12)}`);

        const task = storageRef.put(this.state.pictureFile);

        task.on( (error) => {

            console.log(error);

        },() => {
            console.log(storageRef.name);
            return storageRef.name;

        });

    }

    publish() {

        const acc = JSON.parse(localStorage.getItem(lsUserKey));

        if (acc === null || acc === undefined){

            window.location.href = 'http://localhost:3000/register';
            return;

        }


        const picturePath = this.state.pictureFile!==null?this.uploadImage():null;

        const postBody = JSON.stringify(
        {
                content: this.state.content,
                authorId: acc.id,
                communityId: this.state.communityId,
                lastPostId: null
            }
        );
        fetch(`${devRootURL}${postApiURLs.create}`,{
            method: 'POST',
            headers: {'Content-type': 'application/json;charset=UTF-8'},
            body: postBody
        })
            .then(response => {if(response.status === 200) {

                this.toggleOpenModal(false);

            }
        })
            .catch(err => console.log(err));

    }

    modalWindow(){

        return(
        <section className={"flex flex-col bg-dark w-full h-full md:w-5/6 md:h-3/4 m-auto md:mt-40 text-white py-4 shadow-innerW"}>
            <h3 className={"font-bold text-light text-xl text-center"}>Crear publicación</h3>
           <div className={"flex flex-row justify-between items-center mt-6 md:mt-2"}>
                <button onClick={()=>this.toggleOpenModal()} className={"transform scale-150 ml-8 text-white "}>
                    <img src={backIcon} alt="Back"/>
                </button>
                <button onClick={this.publish}
                    className={"rounded-full w-32 h-8 font-bold text-dark bg-light hover:bg-primary ml-40 mr-4 md:mr-12"}>Publicar</button>
           </div>
            <div className={"flex flex-row border-lg mt-12 h-full md:ml-8 xl:ml-48"}>
                <figure className={"ml-4 mr-4 mt-2"}>
                    <img className={"rounded-full w-12"}
                         src="https://scontent.fmty1-1.fna.fbcdn.net/v/t1.6435-9/46051495_10213185968027882_5149631763173081088_n.jpg?_nc_cat=105&ccb=1-3&_nc_sid=09cbfe&_nc_ohc=BzpWfsQmnTMAX9u6jcp&_nc_ht=scontent.fmty1-1.fna&oh=7957e60b699260398f3e310dcb99f272&oe=608DED45" alt="personaje"/>
                </figure>
                <textarea onChange={this.handleContentChange}
                    className={"w-3/4 h-auto bg-gray-900 py-4 px-2 resize-none"}/>
            </div>
            <div className={"flex flex-row mt-2  w-5/6 justify-around md:justify-between md:ml-16 md:w-2/3 md:self-center md:mr-4 self-end"}>
                <label>
                    <img className={"transform scale-150"} src={picIcon} alt="Imagen"/>
                    <input onChange={this.handlePictureChange}
                        className={"hidden"} type="file" accept="image/png, image/jpeg"/>
                </label>
                <div className={"flex flex-col items-start"}>
                    <label htmlFor="community" className={"text-xs text-light"}>Comunidad</label>
                    <select value={this.state.communityId} onChange={this.handleCommunityChange}
                        className={"bg-gray-900 w-48 h-8 py-1"} name="community" id="community">
                        {this.state.communities.map((com) => {

                            return <option key={com.id} value={com.id}>{com.name}</option>

                        })}
                    </select>
                </div>
            </div>
        </section>
        );
    }

    toggleOpenModal(flag = undefined){


        if(flag === undefined){

            this.setState({

               openModal: !this.state.openModal

            });

        }

        else{

            this.setState({

                openModal: flag

            });
        }

    }

    render(){

        return(
            <section className={"mt-20 md:mt-8 max-h-full w-5/7  md:w-9/12 xl:w-2/5 flex flex-row p-2 bg-dark"}>

                <figure className={"mr-4"}>
                    <img className={"rounded-full w-12 mt-1.5"}
                    src={this.state.picture} alt={this.state.name}/>
                </figure>
                <div onClick={()=>this.toggleOpenModal(true)}
                    className={`w-full bg-gray-900 rounded-full border border-light  pl-4 py-2.5
                hover:bg-light hover:text-dark cursor-text text-gray-300`}>
                    ¿De qué conversamos, {this.props.username}?</div>

                <Modal open={this.state.openModal}>
                    {this.modalWindow()}
                </Modal>
            </section>
        );

    }

}

export default Publisher;
