import { StartNGPage } from './app.po';

describe('start-ng App', () => {
  let page: StartNGPage;

  beforeEach(() => {
    page = new StartNGPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
