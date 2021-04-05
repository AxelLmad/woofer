import React from 'react';
import {Modal} from "@material-ui/core";
import backIcon from '../../../img/icon/arrow-left.svg';
import picIcon from '../../../img/icon/photograph.svg';

class Publisher extends React.Component{

    constructor(props) {
        super(props);

        this.state = {

            openModal: false

        }

        this.toggleOpenModal = this.toggleOpenModal.bind(this);
    }

    modalWindow(){

        return(
        <section className={"flex flex-col bg-dark w-full h-full md:w-5/6 md:h-3/4 m-auto md:mt-40 text-white py-4"}>
            <h3 className={"font-bold text-light text-xl text-center"}>Crear publicación</h3>
           <div className={"flex flex-row justify-between items-center mt-6 md:mt-2"}>
                <button onClick={()=>this.toggleOpenModal()} className={"transform scale-150 ml-8 text-white "}>
                    <img src={backIcon} alt="Back"/>
                </button>
                <button className={"rounded-full w-32 h-8 font-bold text-dark bg-light hover:bg-primary ml-40 mr-4 md:mr-12"}>Publicar</button>
           </div>
            <div className={"flex flex-row border-lg mt-12 h-full md:ml-8 xl:ml-48"}>
                <figure className={"ml-4 mr-4 mt-2"}>
                    <img className={"rounded-full w-12"}
                         src="https://scontent.fmty1-1.fna.fbcdn.net/v/t1.6435-9/46051495_10213185968027882_5149631763173081088_n.jpg?_nc_cat=105&ccb=1-3&_nc_sid=09cbfe&_nc_ohc=BzpWfsQmnTMAX9u6jcp&_nc_ht=scontent.fmty1-1.fna&oh=7957e60b699260398f3e310dcb99f272&oe=608DED45" alt="personaje"/>
                </figure>
                <textarea className={"w-3/4 h-auto bg-gray-900 py-4 px-2 resize-none"}/>
            </div>
            <div className={"flex flex-row mt-2  w-5/6 justify-around md:justify-between md:ml-16 md:w-2/3 md:self-center md:mr-4 self-end"}>
                <button><img className={"transform scale-150"} src={picIcon} alt="Imagen"/></button>
                <div className={"flex flex-col items-start"}>
                    <label htmlFor="community" className={"text-xs text-light"}>Comunidad</label>
                    <select className={"bg-gray-900 w-48 h-8 py-1"} name="community" id="community">
                        <option value="Naruto fans">Naruto fans</option>
                        <option value="Pokemon love">Pokemon Love</option>
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
                    <img className={"rounded-full w-12"}
                    src="https://scontent.fmty1-1.fna.fbcdn.net/v/t1.6435-9/46051495_10213185968027882_5149631763173081088_n.jpg?_nc_cat=105&ccb=1-3&_nc_sid=09cbfe&_nc_ohc=BzpWfsQmnTMAX9u6jcp&_nc_ht=scontent.fmty1-1.fna&oh=7957e60b699260398f3e310dcb99f272&oe=608DED45" alt="personaje"/>
                </figure>
                <div onClick={()=>this.toggleOpenModal(true)}
                    className={`w-full bg-gray-900 rounded-full border border-light  pl-4 pt-2.5
                hover:bg-light hover:text-dark cursor-text text-gray-300`}>
                    ¿De qué conversamos, {this.props.username}?</div>

                <Modal open={this.state.openModal}
                        >
                    {this.modalWindow()}
                </Modal>
            </section>
        );

    }

}

export default Publisher;
