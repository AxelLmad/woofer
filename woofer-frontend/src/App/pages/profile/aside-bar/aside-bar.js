import React from 'react';
import {Modal} from "@material-ui/core";
import SmallList from "../../../shared_components/small-list/small-list";
import {User} from '../../../../models/user';


class AsideBar extends React.Component{

    state = {

        openModal: false,
        selectedList: []

    }

    constructor() {
        super();

        this.toggleOpenModal = this.toggleOpenModal.bind(this);
        this.setFollowers = this.setFollowers.bind(this);
        this.setFollowedUsers = this.setFollowedUsers.bind(this);
        this.setCommunities = this.setCommunities.bind(this);
    }

    setFollowers(){

        this.setState(
            {selectedList: this.props.followers.map(

                    (el) => {

                        return ({name: el.name, picture: el.picture, type: (el instanceof User)?'user':'community'});

                    }

                )}
        );

        this.toggleOpenModal(true);

    }

    setFollowedUsers(){

        this.setState(
            {selectedList: this.props.followedUsers.map(

                    (el) => {

                        return ({name: el.name, picture: el.picture, type: (el instanceof User)?'user':'community'});

                    }

                )}
        );

        this.toggleOpenModal(true);

    }

    setCommunities(){

        this.setState(
            {selectedList: this.props.communities.map(

                    (el) => {

                        return ({name: el.name, picture: el.picture, type: (el instanceof User)?'user':'community'});

                    }

                )}
        );

        this.toggleOpenModal(true);

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

        return(<div className={"xl:flex bg-dark flex-col items-center text-white mr-12 mt-36 border rounded w-64 hidden py-5"}>

            <div className={"pl-8 py-4 mt-4 flex flex-row bg-dark w-full hover:bg-primary hover:text-midnight cursor-pointer"}>
                <span onClick={this.setFollowers}>
                    <span className={"bg-white text-dark px-1"}>{this.props.followers.length}</span>
                    <span className={"underline ml-5"}>Seguidores</span>
                </span>
            </div>

            <div className={"pl-8 py-4 flex flex-row bg-dark w-full hover:bg-primary hover:text-midnight cursor-pointer"}>
                <span onClick={this.setFollowedUsers}>
                    <span className={"bg-white text-dark px-1"}>{this.props.followedUsers.length}</span>
                    <span className={"underline ml-5"}>Seguidos</span>
                </span>
            </div>

            <div className={"pl-8 py-4 flex flex-row bg-dark w-full hover:bg-primary hover:text-midnight cursor-pointer"}>
                <span onClick={this.setCommunities}>
                    <span className={"bg-white text-dark px-1"}>{this.props.communities.length}</span>
                    <span className={"underline ml-5"}>Comunidades</span> </span>
            </div>

            <Modal open={this.state.openModal}>
                {<SmallList closeModal={() => {
                    this.setState({openModal: false});
                }} list={this.state.selectedList}/>}
            </Modal>
        </div>);

    }


}

export default AsideBar;
