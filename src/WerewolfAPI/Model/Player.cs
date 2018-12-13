/* 
 * Werewolf Engine
 *
 * This is a werewolf game engine for REST access. It is primarily developed for CPE200 class at Computer Engineering, Chiang Mai University.
 *
 * OpenAPI spec version: 0.1.0
 * Contact: pruetboonma@gmail.com
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = WerewolfAPI.Client.SwaggerDateConverter;

namespace WerewolfAPI.Model
{
    /// <summary>
    /// Player
    /// </summary>
    [DataContract]
    public partial class Player :  IEquatable<Player>, IValidatableObject
    {
        /// <summary>
        /// Player status in a game
        /// </summary>
        /// <value>Player status in a game</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            
            /// <summary>
            /// Enum Alive for value: alive
            /// </summary>
            [EnumMember(Value = "alive")]
            Alive = 1,
            
            /// <summary>
            /// Enum Offline for value: offline
            /// </summary>
            [EnumMember(Value = "offline")]
            Offline = 2,
            
            /// <summary>
            /// Enum Notingame for value: not in game
            /// </summary>
            [EnumMember(Value = "not in game")]
            Notingame = 3,
            
            /// <summary>
            /// Enum Votedead for value: votedead
            /// </summary>
            [EnumMember(Value = "votedead")]
            Votedead = 4,
            
            /// <summary>
            /// Enum Shotdead for value: shotdead
            /// </summary>
            [EnumMember(Value = "shotdead")]
            Shotdead = 5,
            
            /// <summary>
            /// Enum Jaildead for value: jaildead
            /// </summary>
            [EnumMember(Value = "jaildead")]
            Jaildead = 6,
            
            /// <summary>
            /// Enum Holydead for value: holydead
            /// </summary>
            [EnumMember(Value = "holydead")]
            Holydead = 7,
            
            /// <summary>
            /// Enum Killdead for value: killdead
            /// </summary>
            [EnumMember(Value = "killdead")]
            Killdead = 8
        }

        /// <summary>
        /// Player status in a game
        /// </summary>
        /// <value>Player status in a game</value>
        [DataMember(Name="status", EmitDefaultValue=false)]
        public StatusEnum Status { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Player" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected Player() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Player" /> class.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <param name="Name">Name (required).</param>
        /// <param name="Password">Password (required).</param>
        /// <param name="Game">Game.</param>
        /// <param name="Role">Role.</param>
        /// <param name="Session">Session.</param>
        /// <param name="Status">Player status in a game (required).</param>
        public Player(long? Id = default(long?), string Name = default(string), string Password = default(string), Game Game = default(Game), Role Role = default(Role), string Session = default(string), StatusEnum Status = default(StatusEnum))
        {
            // to ensure "Name" is required (not null)
            if (Name == null)
            {
                throw new InvalidDataException("Name is a required property for Player and cannot be null");
            }
            else
            {
                this.Name = Name;
            }
            // to ensure "Password" is required (not null)
            if (Password == null)
            {
                throw new InvalidDataException("Password is a required property for Player and cannot be null");
            }
            else
            {
                this.Password = Password;
            }
            // to ensure "Status" is required (not null)
            if (Status == null)
            {
                throw new InvalidDataException("Status is a required property for Player and cannot be null");
            }
            else
            {
                this.Status = Status;
            }
            this.Id = Id;
            this.Game = Game;
            this.Role = Role;
            this.Session = Session;
        }
        
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public long? Id { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        private string name;
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name {
            get
            {
                return name;
            }
            set
            {
                if (Name == null || Name == "")
                {
                    throw new InvalidDataException("name cant be Null or \"\"");
                }
                name = value;
            }
        }

        /// <summary>
        /// Gets or Sets Password
        /// </summary>
        [DataMember(Name="password", EmitDefaultValue=false)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or Sets Game
        /// </summary>
        [DataMember(Name="game", EmitDefaultValue=false)]
        public Game Game { get; set; }

        /// <summary>
        /// Gets or Sets Role
        /// </summary>
        [DataMember(Name="role", EmitDefaultValue=false)]
        public Role Role { get; set; }

        /// <summary>
        /// Gets or Sets Session
        /// </summary>
        [DataMember(Name="session", EmitDefaultValue=false)]
        public string Session { get; set; }


        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Player {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Password: ").Append(Password).Append("\n");
            sb.Append("  Game: ").Append(Game).Append("\n");
            sb.Append("  Role: ").Append(Role).Append("\n");
            sb.Append("  Session: ").Append(Session).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as Player);
        }

        /// <summary>
        /// Returns true if Player instances are equal
        /// </summary>
        /// <param name="input">Instance of Player to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Player input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.Password == input.Password ||
                    (this.Password != null &&
                    this.Password.Equals(input.Password))
                ) && 
                (
                    this.Game == input.Game ||
                    (this.Game != null &&
                    this.Game.Equals(input.Game))
                ) && 
                (
                    this.Role == input.Role ||
                    (this.Role != null &&
                    this.Role.Equals(input.Role))
                ) && 
                (
                    this.Session == input.Session ||
                    (this.Session != null &&
                    this.Session.Equals(input.Session))
                ) && 
                (
                    this.Status == input.Status ||
                    (this.Status != null &&
                    this.Status.Equals(input.Status))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.Name != null)
                    hashCode = hashCode * 59 + this.Name.GetHashCode();
                if (this.Password != null)
                    hashCode = hashCode * 59 + this.Password.GetHashCode();
                if (this.Game != null)
                    hashCode = hashCode * 59 + this.Game.GetHashCode();
                if (this.Role != null)
                    hashCode = hashCode * 59 + this.Role.GetHashCode();
                if (this.Session != null)
                    hashCode = hashCode * 59 + this.Session.GetHashCode();
                if (this.Status != null)
                    hashCode = hashCode * 59 + this.Status.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
