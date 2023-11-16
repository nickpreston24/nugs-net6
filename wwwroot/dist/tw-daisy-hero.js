 // Lego version 2.0.0-beta.8
  import { h, Component } from './lego.min.js'
  

  

  const __template = function({ state }) {
    return [  
    h("div", {"part": `root`}, [
      h("div", {"root": `content`}, [
        h("img", {"src": `https://daisyui.com/images/stock/photo-1635805737707-575885ab0820.jpg`, "class": `max-w-sm rounded-lg shadow-2xl`}, ""),
        h("div", {}, [
          h("h1", {"class": `text-5xl font-bold`}, `Box Office News!`),
          h("p", {"class": `py-6`}, `Provident cupiditate voluptatem et in. Quaerat fugiat ut assumenda excepturi
                    exercitationem quasi. In deleniti eaque aut repudiandae et a id nisi.`),
          h("button", {"class": `btn btn-primary`}, `Get Started`)
        ])
      ])
    ])
  ]
  }

  const __style = function({ state }) {
    return h('style', {}, `
      
      
        daisy-hero::part(root) {
            @apply hero min-h-screen bg-base-200;
        }
    
        daisy-hero::part(content) {
            @apply hero-content flex-col lg:flex-row;
        }
    
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

  
