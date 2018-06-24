import { DefaultUrlSerializer, UrlTree } from '@angular/router';
// URL Case sensitive issue
export class LowerCaseUrlSerializer extends DefaultUrlSerializer {
    parse(url: string): UrlTree {
        return super.parse(url.toLowerCase());
    }
}
