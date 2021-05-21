import React from 'react';


class AsideBar extends React.Component{

    constructor() {
        super();
    }

    render(){

        return(<div className={"xl:flex bg-dark flex-col items-center text-white mr-12 mt-36 border rounded w-64 hidden py-5"}>

            <button className={"rounded-full w-2/3 h-12 font-bold text-dark bg-light mx-auto mt-4 hover:bg-primary"}>
                Crear comunidad
            </button>

            <div className={"pl-8 py-4 mt-4 flex flex-row bg-dark w-full hover:bg-primary hover:text-midnight cursor-pointer"}>
                <span className={"underline"}>Comunidades seguidas</span>
            </div>

            <div className={"pl-8 py-4 flex flex-row bg-dark w-full hover:bg-primary hover:text-midnight cursor-pointer"}>
                <span className={"underline"}>Mis comunidades</span>
            </div>

            <h4 className={"text-center mt-5 mb-3"}>Sugerencias</h4>

            <article className={"flex flex-col items-center bg-dark w-full"}>
                <h5>Comunidad</h5>
                <figure><img className={"w-1/3 mx-auto"} src="https://i.pinimg.com/originals/2f/a6/fa/2fa6fa2b399f3d85d8f403d4ac5ec666.png" alt="comunidad"/></figure>
                <button className={"rounded-full w-2/3 h-6 font-bold text-dark bg-light mx-auto mt-4 hover:bg-primary"}>
                    Seguir
                </button>
            </article>

            <article className={"flex flex-col items-center bg-dark w-full"}>
                <h5>Comunidad</h5>
                <figure><img className={"w-1/3 mx-auto"} src="https://i.pinimg.com/originals/2f/a6/fa/2fa6fa2b399f3d85d8f403d4ac5ec666.png" alt="comunidad"/></figure>
                <button className={"rounded-full w-2/3 h-6 font-bold text-dark bg-light mx-auto mt-4 hover:bg-primary"}>
                    Seguir
                </button>
            </article>

            <article className={"flex flex-col items-center bg-dark w-full"}>
                <h5>Comunidad</h5>
                <figure><img className={"w-1/3 mx-auto"} src="https://i.pinimg.com/originals/2f/a6/fa/2fa6fa2b399f3d85d8f403d4ac5ec666.png" alt="comunidad"/></figure>
                <button className={"rounded-full w-2/3 h-6 font-bold text-dark bg-light mx-auto mt-4 hover:bg-primary"}>
                    Seguir
                </button>
            </article>

        </div>);

    }


}

export default AsideBar;
