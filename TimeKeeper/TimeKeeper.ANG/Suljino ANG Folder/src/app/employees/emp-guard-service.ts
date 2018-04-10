import { Injectable, Inject } from "@angular/core";
import { CanActivate } from "@angular/router";
import { Router } from "@angular/router";
import { ActivatedRouteSnapshot } from "@angular/router";
import { isString } from "util";

@Injectable()

export class EmpGuardService implements CanActivate{
    constructor(private router: Router){ }

    canActivate(route: ActivatedRouteSnapshot):boolean{
        let id = +route.url[1].path;
        if(isNaN(id)||id<1){
            alert("Invalid employee id");
            this.router.navigate(["/empl"]);
            return false;
        }
        return true;
    }
}