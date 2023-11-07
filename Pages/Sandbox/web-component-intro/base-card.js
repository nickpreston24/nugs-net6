//This file will be the web component
//It only needs to run, not be imported by main.js
const template = document.createElement('template');
template.innerHTML = `
  <div class='flex flex-col'>
    <h1>Base Card</h1>
    <slot name="title">Default text if not title slot used in HTML</slot>
    <slot name="list"></slot>
  </div>
`;

class BaseCard extends HTMLElement {
  constructor() {
    super();
    const shadowRoot = this.attachShadow({ mode: 'closed' });
    // let div = document.createElement('div');
    // div.textContent = 'Base Card Theory';
    // shadowRoot.append(div);
    let clone = template.content.cloneNode(true);
    shadowRoot.append(clone);
  }
}

customElements.define('base-card', BaseCard);
// <base-card>

