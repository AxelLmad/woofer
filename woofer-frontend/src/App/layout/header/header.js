import React from 'react';
import { Link } from "react-router-dom";
import searchIcon from '../../../img/icon/search.svg';
import userIcon from '../../../img/icon/user.svg';
import logoIcon from '../../../logo.svg';
import Modal from "@material-ui/core/Modal";
import {communityApiURLs, devRootURL, userApiURLs, userPictureApiURLs} from "../../constants/api-url";
import SmallList from "../../shared_components/small-list/small-list";
import {Community} from "../../../models/community";
import {User} from "../../../models/user";
import MenuItem from "@material-ui/core/MenuItem";
import Menu from "@material-ui/core/Menu";
import {currentIP, lsUserKey} from "../../constants/keys";

class Header extends React.Component{

    state = {

        openSearchModal: false,
        searchData: '',
        users: [],
        communities: [],
        anchorEl: null

    }

    constructor(props) {
        super(props);

        this.handleClickSearch = this.handleClickSearch.bind(this);
        this.handleClose = this.handleClose.bind(this);
    }

    handleClose () {
        this.setState({anchorEl: null});
    };

    handleClickSearch(){

        if(this.state.searchData === '' || this.state.searchData === undefined || this.state.searchData === null){
            return;
        }

        fetch(`${devRootURL}${userApiURLs.byNickName(this.state.searchData)}`,{
            method: 'GET'
        })
            .then(response => response.json())
            .then((json) => {
                console.log(json)
                const us = json.map((el) => {

                    return new User({id: el.id, picture: el.picture, name: el.name, nickname: el.nickName,
                    email: el.email, lastName: el.lastName});

                });
                this.setState({users: [...us]});

                fetch(`${devRootURL}${communityApiURLs.byName(this.state.searchData)}`,{
                    method: 'GET'
                })
                    .then(response => response.json())
                    .then((json) => {
                        const com = json.map((el)=>{

                            return new Community({id: el.id, name: el.name, picture: el.picture,
                                creationDate: el.creationDate, description: el.description, authorId: el.ownerId,
                                color: el.color});

                        });

                        this.setState({communities: [...com], openSearchModal: true});
                    })
                    .catch(err => console.log(err));

            })
            .catch(err => console.log(err));

    }

    render(){
        return(
            <header className={"h-16 w-screen bg-dark border border-gray-600 border-t-0 border-l-0 border-r-0 border-1 flex flex-row justify-between fixed z-50"}>
                <div className={"flex flex-row justify-around items-end md:ml-8"}>
                <Link to={"/"}>
                    <figure><img className={"w-20"} src={logoIcon} alt="Woofer"/></figure>
                </Link>
                </div>
                <div className={"flex flex-row justify-start items-center mr-4"}>
                    <input onChange={($e)=>{
                        this.setState({searchData: $e.target.value})
                    }}
                        name={"search-bar"} type="text" className={"ml-2 md:ml-8 w-32 md:w-96 h-8 rounded outline-none px-4"}/>
                    <figure onClick={this.handleClickSearch}
                        className={"bg-light transform -translate-x-2 rounded hover:bg-primary"}>
                        <img src={searchIcon} alt="Search" className={"cursor-pointer h-8 w-8"}/>
                    </figure>
                </div>
                <nav className={"flex flex-row justify-around items-end mr-2 md:mr-8 mb-4"}>

                    <figure onClick={(event)=>{this.setState({anchorEl: event.currentTarget})}}
                        className={"bg-light cursor-pointer ml-1 md:ml-2 hover:bg-primary md:w-12 md:h-8 rounded-full"}>
                        <img className={"transform md:scale-150 mx-auto md:mt-2"} src={userIcon} alt="Usuario"/>
                    </figure>

                    <Menu
                        anchorEl={this.state.anchorEl}
                        keepMounted
                        open={Boolean(this.state.anchorEl)}
                        onClose={this.handleClose}
                    >
                        <MenuItem onClick={() => {
                            localStorage.removeItem(lsUserKey);
                            this.handleClose();
                            window.location.href = `http://${currentIP}/login`
                        }}>Cerrar sesión</MenuItem>

                    </Menu>
                </nav>

                <Modal open={this.state.openSearchModal}>
                    <SmallList closeModal={() => {
                        this.setState({openSearchModal: false});
                    }} list={this.state.users.concat(this.state.communities)}
                        title={`Resultados de buscar '${this.state.searchData}'`}/>
                </Modal>
            </header>
        );
    }
}
export default Header;
