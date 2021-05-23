import React from 'react';
import {devRootURL, userPictureApiURLs} from "../../constants/api-url";
import firebase from "firebase";
import {currentIP} from "../../constants/keys";

class SmallListCard extends React.Component{

    state = {
        picture: ''
    }


    constructor(props) {
        super(props);

            this.linkCriteria = this.linkCriteria.bind(this);

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

    linkCriteria(){

        return this.props.type==='user'?`profile/${this.props.nickname}`:`community/${this.props.name}/${this.props.id}`;

    }

    render(){

        return <article className={`flex flex-col items-center bg-dark border border-white w-5/6 mx-auto my-5 mx-3 py-2
        ${this.props.small?'px-2':'lg:w-1/3'}
        `}>
            <h3 className={"font-bold text-primary"}>{this.props.type==='user'?this.props.nickname:this.props.name}</h3>
            <h5 className={`mb-2 ${this.props.color}`}>{this.props.description}</h5>
            <figure className={this.props.small?'w-36':'w-2/3'}><img className={"mx-auto"} src={this.state.picture} alt={this.props.type}/></figure>
            <button onClick={()=>{window.location.href = `http://${currentIP}/${this.linkCriteria()}`;}}
                className={"rounded-full w-2/3 h-6 font-bold text-dark bg-light mx-auto mt-4 hover:bg-primary"}>
                Ver
            </button>
        </article>;

    }

}

export default SmallListCard;
