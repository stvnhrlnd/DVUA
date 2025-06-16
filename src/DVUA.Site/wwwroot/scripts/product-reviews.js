class ProductReviewsComponent extends HTMLElement {
  connectedCallback() {
    if (this.querySelector("h2")) return;

    const umbracoNodeId = this.getAttribute("umbraco-node-id");
    const isLoggedIn = this.getAttribute("is-logged-in");
    const redirectUrl = this.getAttribute("redirect-url");

    this.innerHTML = `
      <h2>Reviews</h2>

      <form ${isLoggedIn || "hidden"}>
        <label class="form-label" for="comment">Comment</label>
        <textarea class="form-control" id="comment" name="comment" required></textarea>
        <input type="hidden" name="umbracoNodeId" value="${umbracoNodeId}">
        <button type="submit">Submit</button>
      </form>

      <p ${!isLoggedIn || "hidden"}>
        <a href="/login/?redirectUrl=${redirectUrl}">Sign in</a> to leave a review.
      </p>

      <div id="review-list">
      </div>
    `;

    this.querySelector("form").onsubmit = (e) => {
      e.preventDefault();

      const data = new FormData(e.target);
      const entries = Object.fromEntries(data.entries());
      entries.comment = DOMPurify.sanitize(entries.comment);

      fetch("/api/reviews", {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(entries),
      }).then((response) => {
        this.update();
        e.target.reset();
      });
    };

    this.update();
  }

  update() {
    const umbracoNodeId = this.getAttribute("umbraco-node-id");
    const reviewListElement = this.querySelector("#review-list");

    // TODO: Encode comments to prevent XSS
    fetch("/api/reviews?umbracoNodeId=" + umbracoNodeId)
      .then((response) => response.json())
      .then(
        (reviews) =>
          (reviewListElement.innerHTML = reviews
            .map(
              (review) => `
                <hr style="margin-block: 2em;">
                <div>
                  <h3>${review.memberDisplayName}</h3>
                  ${review.comment}
                </div>
              `
            )
            .join(""))
      );
  }
}

customElements.define("product-reviews", ProductReviewsComponent);
