 // Lego version 2.0.0-beta.7
  import { h, Component } from './lego.min.js'
  

  

  const __template = function({ state }) {
    return [  
    h("div", {"class": `screen`}, [
      h("div", {"class": `screen-image`, "style": `background-image: url('https://images.unsplash.com/photo-1580273916550-e323be2ae537?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=764&q=80')`}, ""),
      h("div", {"class": `screen-overlay`}, ""),
      h("div", {"class": `screen-content`}, [
        h("i", {"class": `screen-icon fa-brands fa-codepen`}, ""),
        h("div", {"class": `screen-user`}, [
          h("span", {"class": `name`, "data-value": `car_name`}, `Name Here`),
          h("a", {"class": `link`, "href": `https://carcapital.com/`, "target": `_blank`}, `Car Capital`)
        ])
      ])
    ]),
    h("div", {"hidden": ``, "x-show": `false`, "id": `blob`}, ""),
    h("div", {"hidden": ``, "x-show": `false`, "id": `blur`}, ""),
    h("div", {"id": `links`}, [
      h("div", {"hidden": ``, "x-show": `false`, "class": `meta-link`}, [
        h("span", {"class": `label`}, `Futuristic Card Effect`),
        h("span", {"class": `dot`}, `·`),
        h("a", {"class": `source`, "href": `https://www.sprite.com/zerolimits`, "target": `_blank`}, [
          h("i", {"class": `fa-solid fa-link`}, "")
        ]),
        h("span", {"class": `dot`}, `·`),
        h("a", {"class": `youtube`, "href": `https://youtu.be/jMVhxBB3l0w`, "target": `_blank`}, [
          h("i", {"class": `fa-brands fa-youtube`}, "")
        ])
      ]),
      h("div", {"hidden": ``, "x-show": `false`, "class": `meta-link`}, [
        h("span", {"class": `label`}, `Glowy Blob Effect`),
        h("span", {"class": `dot`}, `·`),
        h("a", {"class": `source`, "href": `https://www.poppr.be`, "target": `_blank`}, [
          h("i", {"class": `fa-solid fa-link`}, "")
        ]),
        h("span", {"class": `dot`}, `·`),
        h("a", {"class": `youtube`, "href": `https://youtu.be/kySGqoU7X-s`, "target": `_blank`}, [
          h("i", {"class": `fa-brands fa-youtube`}, "")
        ])
      ]),
      h("div", {"hidden": ``, "x-show": `false`, "class": `meta-link`}, [
        h("span", {"class": `label`}, `Text Effect`),
        h("span", {"class": `dot`}, `·`),
        h("a", {"class": `source`, "href": `https://kprverse.com`, "target": `_blank`}, [
          h("i", {"class": `fa-solid fa-link`}, "")
        ]),
        h("span", {"class": `dot`}, `·`),
        h("a", {"class": `youtube`, "href": `https://youtu.be/W5oawMJaXbU`, "target": `_blank`}, [
          h("i", {"class": `fa-brands fa-youtube`}, "")
        ])
      ])
    ])
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

  
