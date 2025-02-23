window.downloadPdf = (base64, fileName) => {
    const link = document.createElement("a");
    link.href = "data:application/pdf;base64," + base64;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};
    