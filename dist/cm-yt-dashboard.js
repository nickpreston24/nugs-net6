 // Lego version 2.0.0-beta.8
  import { h, Component } from './lego.min.js'
  

  

  const __template = function({ state }) {
    return [  
`
    

/* from: https://www.youtube.com/watch?v=NnniXasJIpY */

`,
    h("div", {"id": `dash1`, "class": `border-red-400 border-2`}, [
      h("div", {"class": `header`}, `header`),
      h("div", {"class": `sidebar`}, `sidebar`),
      h("div", {"class": `main`}, [
        h("div", {"class": `card text-orange-500 sm:min-w-36`}, `One`),
        h("div", {"class": `card text-orange-500 sm:min-w-36`}, `Two`),
        h("div", {"class": `card text-orange-500 sm:min-w-36`}, `Three`),
        h("div", {"class": `card text-orange-500 sm:min-w-36`}, `4`),
        h("div", {"class": `card text-orange-500 sm:min-w-36`}, `5`),
        h("div", {"class": `card text-orange-500 sm:min-w-36`}, `6`)
      ])
    ]),
    h("style", {}, `

    #dash1 {
        height: 100vh;

        grid-template-columns: 200px 3fr;
        grid-template-rows: 60px 1fr;

        display: grid;
    }

    .header {
        background-color: #fff;
        grid-column: 2/3;
    }

    .sidebar {
        /*background-color: #95ff7c;*/
        grid-column: 1/2;
        grid-row: 1/3;
    }

    .main {
        display: grid;
        background-color: #c3c5ca;
        padding: 25px;

        grid-template-columns: 1fr 1fr 1fr;
        grid-template-rows: 1fr 1fr 1fr;

        grid-template-areas: 
        "c1 c2 c3"
        "c4 c4 c5"
        "c4 c4 c6";

        gap: 20px;
    }

    .card {
        background-color: #f6f7f9;
        border-radius: 10px;
    }

    .card:nth-child(1) {
        grid-area: c1;
    }

    .card:nth-child(2) {
        grid-area: c2;
    }

    .card:nth-child(3) {
        grid-area: c3;
    }

    .card:nth-child(4) {
        grid-area: c4;
    }


`)
  ]
  }

  const __style = function({ state }) {
    return h('style', {}, `
      
      
    `)
  }

  // -- Lego Core
  export default class Lego extends Component {
    init() {
      this.useShadowDOM = true
      if(typeof state === 'object') this.__state = Object.assign({}, state, this.__state)
      if(typeof connected === 'function') this.connected = connected
      if(typeof setup === 'function') setup.bind(this)()
    }
    get vdom() { return __template }
    get vstyle() { return __style }
  }
  // -- End Lego Core

  
