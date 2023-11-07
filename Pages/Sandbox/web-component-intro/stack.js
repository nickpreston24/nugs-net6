//This file will be the web component
//It only needs to run, not be imported by main.js
const template = document.createElement('template');
template.innerHTML = `
  <div class='flex flex-col items-center gap-2 justify-center'>    
    <slot name="render"></slot>
  </div>
`;

class Stack extends HTMLElement {
  constructor() {
    super();
    const shadowRoot = this.attachShadow({ mode: 'closed' });
    // let div = document.createElement('div');
    // div.textContent = 'Blarg';
    // shadowRoot.append(div);
    let clone = template.content.cloneNode(true);
    shadowRoot.append(clone);
  }
}

customElements.define('stack', Stack);
// <stack>

