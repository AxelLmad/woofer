import React from 'react';
import backIcon from "../../../img/icon/arrow-left.svg";
import SmallListCard from "./small-list-card";
import {User} from '../../../models/user';
import {Community} from "../../../models/community";


class SmallList extends React.Component{

    constructor() {
        super();

        this.toggleOpenModal = this.toggleOpenModal.bind(this);
    }

    state = {
        openModal: false
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

    renderCards() {
        let key = 0;
        return this.props.list.map((el) => {

            return (<li key={key++}><SmallListCard id={el.id} nickname={el.nickname} name={el.name} description={el.description} color={el.color}
                                   type={(el instanceof User)?'user':'community'}
                                    picture={el.picture}
            /></li>);

    });
    }

    render(){

        return <div>

            <section className={"flex flex-col bg-dark w-full h-full md:w-5/6 md:h-3/4 m-auto md:mt-40 text-white py-4 shadow-innerW"}>
                <h3 className={"font-bold text-light text-xl text-center"}>{this.props.title}</h3>
                <div className={"flex flex-row justify-between items-center mt-6 md:mt-2"}>
                    <button onClick={this.props.closeModal} className={"transform scale-150 ml-8 text-white "}>
                        <img src={backIcon} alt="Back"/>
                    </button>

                </div>

                <div className={"flex flex-col"}>
                    <ul className={"list-none"}>
                        {this.renderCards()}
                    </ul>
                </div>

            </section>

                </div>;

    }

}

export default SmallList;
