export function generateContent(element, previewElement, language) {
    let html = element.innerHTML;
    // let doc = new DOMParser().parseFromString(html, "text/html");
    previewElement.innerHTML = html;
    Prism.highlightElement(previewElement);
    // let content = Prism.highlight(doc.documentElement.textContent, Prism.languages[language], language);
}