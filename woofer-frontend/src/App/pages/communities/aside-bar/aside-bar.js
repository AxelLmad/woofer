import React from 'react';
import {Modal} from "@material-ui/core";
import CreateCommunity from "./modals/createCommunity";
import SmallList from "../../../shared_components/small-list/small-list";
import {
    devRootURL,
    communityApiURLs, followedCommunityApiURLs,
} from "../../../constants/api-url";
import {lsUserKey} from "../../../constants/keys";
import {Community} from "../../../../models/community";
import { Link } from "react-router-dom";
import firebase from "firebase";

class AsideBar extends React.Component{

    state = {

        modalCreateCommunity: false,
        modalMyCommunities: false,
        modalFollowedCommunities: false,
        myCommunities: [],
        followedCommunities: [],
        randomCommunities: []

    }

    constructor() {
        super();
        const acc = JSON.parse(localStorage.getItem(lsUserKey));

        fetch(`${devRootURL}${communityApiURLs.getUserCreated(acc.id)}`,{
            method: 'GET'
        })
            .then(response => response.json())
            .then((json)=>{
                console.log(json)
                this.setState({
                    myCommunities: json.map((el) => {

                        return new Community({id: el.id, name: el.name, color: el.color, description: el.description,
                            picture: el.picture, creationDate: el.creationDate, authorId: el.ownerId})

                    })

                });

            })
            .catch(err => console.log(err));

        fetch(`${devRootURL}${followedCommunityApiURLs.getByFollower(acc.id)}`,{
            method: 'GET'
        })
            .then(response => response.json())
            .then((json)=>{

                this.setState({
                    followedCommunities: json.map((el) => {

                        return new Community({id: el.id, name: el.name, color: el.color, description: el.description,
                            picture: el.picture, creationDate: el.creationDate, authorId: el.ownerId})

                    })

                });

            })
            .catch(err => console.log(err));


        fetch(`${devRootURL}${communityApiURLs.random}`,{
            method: 'GET'
        })
            .then(response => response.json())
            .then((json)=>{
                this.setState({randomCommunities: json.map(

                        (el) => {

                           return new Community({id: el.id, name: el.name, color: el.color, description: el.description,
                                picture: el.picture, creationDate: el.creationDate, authorId: el.ownerId})

                        }

                    )})

            })
            .catch(err => console.log(err));



    }

    renderRandom(){

        let key = 0;

        return this.state.randomCommunities.map((el)=>{

            return <article key={key++} className={"flex flex-col items-center bg-dark w-full"}>
                <h5>{el.name}</h5>
                <figure><img className={"w-1/3 mx-auto"} src={el.picture} alt={el.name}/></figure>
                <Link to={``}
                      className={"pl-14 rounded-full w-2/3 h-6 font-bold text-dark bg-light mx-auto mt-4 hover:bg-primary"}>
                    Ver
                </Link>
            </article>

    });

    }

    render(){

        return(<div className={"xl:flex bg-dark flex-col items-center text-white mr-12 mt-36 border rounded w-64 hidden py-5"}>

            <button onClick={() => this.setState({modalCreateCommunity: true})}
                className={"rounded-full w-2/3 h-12 font-bold text-dark bg-light mx-auto mt-4 hover:bg-primary"}>
                Crear comunidad
            </button>

            <div onClick={()=>{this.setState({modalFollowedCommunities: true})}}
                className={"pl-8 py-4 mt-4 flex flex-row bg-dark w-full hover:bg-primary hover:text-midnight cursor-pointer"}>
                <span className={"underline"}>Comunidades seguidas</span>
            </div>

            <div onClick={()=>{this.setState({modalMyCommunities: true})}}
                className={"pl-8 py-4 flex flex-row bg-dark w-full hover:bg-primary hover:text-midnight cursor-pointer"}>
                <span className={"underline"}>Mis comunidades</span>
            </div>

            <h4 className={"text-center mt-5 mb-3"}>Sugerencias</h4>

            {

                    this.renderRandom()



            }



            <Modal open={this.state.modalCreateCommunity}>
                {<CreateCommunity closeModal={() => {this.setState({modalCreateCommunity: false})}}/>}
            </Modal>

            <Modal open={this.state.modalMyCommunities}>
                {<SmallList list={this.state.myCommunities} closeModal={() => {this.setState({modalMyCommunities: false})}}/>}
            </Modal>

            <Modal open={this.state.modalFollowedCommunities}>
                {<SmallList list={this.state.followedCommunities} closeModal={() => {this.setState({modalFollowedCommunities: false})}}/>}
            </Modal>
        </div>);

    }


}

export default AsideBar;
