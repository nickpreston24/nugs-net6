 // Lego version 2.0.0-beta.8
  import { h, Component } from './lego.min.js'
  

  

  const __template = function({ state }) {
    return [  
    h("div", {"id": `dash1`, "class": ``}, [
      h("slot", {"name": `header`, "class": `header`}, `Default Header`),
      h("div", {"class": `sidebar`}, [
        h("slot", {"name": `sidebar`}, `sidebar`)
      ]),
      h("div", {"class": `main`}, [
        h("div", {"class": `card sm:min-w-36`}, [
          h("slot", {"name": `place1`}, `One`)
        ]),
        h("div", {"class": `card sm:min-w-36`}, [
          h("slot", {"name": `place2`}, `Two`)
        ]),
        h("div", {"class": `card sm:min-w-36`}, [
          h("slot", {"name": `place3`}, `Three`)
        ]),
        h("div", {"class": `card sm:min-w-36`}, [
          h("slot", {"name": `place4`}, `Four`)
        ]),
        h("div", {"class": `card sm:min-w-36`}, [
          h("slot", {"name": `place5`}, `Five`)
        ]),
        h("div", {"class": `card sm:min-w-36`}, [
          h("slot", {"name": `place6`}, `Six`)
        ])
      ])
    ]),
    h("style", {}, `
    /*
        Temporary stylings in lieu of Tailwind:
    */

    * {
      --gray-blue: #1e293b;
      --light-gray-blue: #b4c6ef;
    }

    #dash1 {
      height: 100vh;
      grid-template-columns: 200px 3fr;
      grid-template-rows: 60px 1fr;

      display: grid;
    }

    .header {
      background-color: var(--light-gray-blue);
      grid-column: 2/3;
    }

    .sidebar {
      grid-column: 1/2;
      grid-row: 1/3;
      /*border: #49ff18 1px solid;*/
    }

    .main {
      display: grid;
      background-color: var(--light-gray-blue);
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
      /* background-color: #f6f7f9; */
      background-color: var(--gray-blue);
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

  
