import React from 'react';
import {devRootURL, userPictureApiURLs} from "../../constants/api-url";
import firebase from "firebase";

class SmallListCard extends React.Component{

    state = {
        picture: ''
    }


    constructor(props) {
        super(props);

        if (this.props.type === 'user'){

            let nullPicture = false;

            fetch(`${devRootURL}${userPictureApiURLs.byUserId(this.props.id)}`,{
                method: 'GET'
            })
                .then(response => response.status===200?response.json():nullPicture = true)
                .then((json)=>{
                    if(!nullPicture){

                        const storageRef = firebase.storage().ref();
                        const starsRef = storageRef.child(json.serverPath);

                        starsRef.getDownloadURL().then((url) => {
                            this.setState({picture: url});
                        }).catch(function(error) {

                            switch (error.code) {
                                case 'storage/object-not-found':
                                    console.log('Object does not exist');
                                    break;
                            }
                        });
                    }
                })
                .catch(err => console.log(err));

        }

        else{
            console.log(this.props.picture);
            if (this.props.picture !== undefined && this.props.picture !== null) {

                const storageRef = firebase.storage().ref();
                const starsRef = storageRef.child(this.props.picture);

                starsRef.getDownloadURL().then((url) => {
                    this.setState({picture: url});
                }).catch(function (error) {

                    switch (error.code) {
                        case 'storage/object-not-found':
                            console.log('Object does not exist');
                            break;
                    }
                });
            }
        }
    }


    render(){

        return <article className={"flex flex-col items-center bg-dark w-full"}>
            <h3 className={"font-bold text-primary"}>{this.props.name}</h3>
            <h5 className={`mb-2 ${this.props.color}`}>{this.props.description}</h5>
            <figure><img className={"w-1/3 mx-auto"} src={this.state.picture} alt={this.props.type}/></figure>
            <button className={"rounded-full w-2/3 h-6 font-bold text-dark bg-light mx-auto mt-4 hover:bg-primary"}>
                Seguir
            </button>
        </article>;

    }

}

export default SmallListCard;
