import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { TournamentIndexComponent } from './pages/tournament-index/tournament-index.component';
import { RegisterMemberComponent } from './pages/register-member/register-member.component';
import { TournamentComponent } from './pages/tournament/tournament.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { ConnexionComponent } from './pages/connexion/connexion.component';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'member/add', component: RegisterMemberComponent },
    { path: 'tournament/index', component: TournamentIndexComponent},
    { path: 'tournament/add', component: TournamentComponent },
    { path: 'member/profile', component: ProfileComponent },
    { path: 'auth/login', component: ConnexionComponent}
];
