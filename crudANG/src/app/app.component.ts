import { Component, OnInit } from '@angular/core';
import { UserService } from './user.service';
import { User } from './model/user.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    users: User[] = [];
    newUser: User = {} as User;
    isEditing = false;
    isConfirming = false; 
    showUserList = false;
  
    constructor(private userService: UserService) { }
  
    ngOnInit(): void {
      this.getUsers();
    }
  
    getUsers(): void {
      this.userService.getUsers()
        .subscribe(users => this.users = users);
    }
  
    addUser(): void {
        if (this.isConfirming) {
            this.updateUser(this.newUser); 
        } else {
            this.userService.addUser(this.newUser)
              .subscribe(
                (user: User) => {
                  this.users.push(user); 
                  this.newUser = {} as User;
                  console.log('Usuário adicionado com sucesso:', user);
                },
                error => {
                  console.error('Erro ao adicionar usuário:', error);
                }
              );
        }
    }
  
    updateUser(user: User): void {
        this.userService.updateUser(user)
          .subscribe(
            () => {
              console.log('Usuário atualizado com sucesso:', user);
              this.isEditing = false;
              this.isConfirming = false;
              this.getUsers();
            },
            error => {
              console.error('Erro ao atualizar usuário:', error);
            }
          );
      }
  
    deleteUser(id: number): void {
      this.userService.deleteUser(id)
        .subscribe(
          () => {
            console.log('Usuário excluído com sucesso:', id);
            this.users = this.users.filter(user => user.id !== id);
          },
          error => {
            console.error('Erro ao excluir usuário:', error);
          }
        );
    }
  
    editUser(user: User): void {
        this.isEditing = true;
        this.newUser = { ...user };
        this.newUser.id = user.id; 
        this.isConfirming = true; 
      }
  
    cancelEdit(): void {
      this.isEditing = false;
      this.newUser = {} as User;
      this.isConfirming = false; 
    }

    toggleUserList() {
        this.showUserList = !this.showUserList;
    }
}
