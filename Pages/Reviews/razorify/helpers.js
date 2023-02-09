const _with = (obj = {}, action = () => {}) => {
  return action(obj);
};

const prepend = (text = "", value = "") => `${value}${text}`;

function capitalize(s) {
  return s[0].toUpperCase() + s.slice(1);
}

const tag = (text = "", tag = "") => {
  if (tag === "input")
    return `<input type="text" value="..." placeholder="..." />`;
  return `<${tag}>${text}</${tag}>`;
};

exports._with = _with;
exports.capitalize = capitalize;
exports.prepend = prepend;
