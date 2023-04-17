// import htm from 'htm'
// import htm from 'https://unpkg.com/htm?module'
// const html = htm.bind(React.createElement);

// import h from 'vhtml'

// const html = htm.bind(h)

// just want htm + preact in a single file? there's a highly-optimized version of that:

import { html, render } from 'https://unpkg.com/htm/preact/standalone.module.js'

export const mount = (App, el) => {
  el.innerHTML = html`<${App} />`
}

window.html = html
window.mount = mount
window.render = render
