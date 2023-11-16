 // Lego version 2.0.0-beta.8
  import { h, Component } from './lego.min.js'
  

  
    const state = {
        registered: false,
        firstName: 'John',
        lastName: 'Doe',
        fruits: [{name: 'Apple', icon: '🍎'}, {name: 'Pineapple', icon: '🍍'}]
    }

    function register() {
        render({registered: confirm('You are about to register…')})
    }


  const __template = function({ state }) {
    return [  
    h("h1", {}, `${ state.firstName } ${ state.lastName }'s profile`),
    h("p", {}, `Welcome ${ state.firstName }!`),
    ((state.fruits.length) ? h("section", {}, [
      h("h3", {}, `The best ${ state.fruits.length } fruit you like:`),
      h("ul", {}, [
        ((state.fruits).map((fruit) => (h("li", {}, `${ fruit.name } ${ fruit.icon }`))))
      ])
    ]) : ''),
    ((state.registered) ? h("p", {}, `You are registered!`) : ''),
    h("button", {"onclick": (typeof register === 'function' ? register.bind(this) : this.register).bind(this)}, `Register now`)
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

  
