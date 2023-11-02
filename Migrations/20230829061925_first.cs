using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillAssessment.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "answer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_answer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Topic_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Topic_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Topic_Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    User_LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    User_Departmenr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    User_Designation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    User_DOB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User_Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User_Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    User_Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    User_EduLevel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    User_PhoneNo = table.Column<long>(type: "bigint", nullable: true),
                    User_Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    User_Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User_Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_ID);
                });

            migrationBuilder.CreateTable(
                name: "levels",
                columns: table => new
                {
                    LevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopicsTopic_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_levels", x => x.LevelId);
                    table.ForeignKey(
                        name: "FK_levels_Topics_TopicsTopic_Id",
                        column: x => x.TopicsTopic_Id,
                        principalTable: "Topics",
                        principalColumn: "Topic_Id");
                });

            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    Assessment_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Assessment_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Assessment_SelectedTopic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Assessment_SelectedLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Assessment_Points = table.Column<int>(type: "int", nullable: false),
                    Assessment_TimeDuration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Assessment_Requested_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Assessment_DateOfCompletion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Assesment_Departmenr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Assessment_NoOfQuestions = table.Column<int>(type: "int", nullable: false),
                    TopicsTopic_Id = table.Column<int>(type: "int", nullable: true),
                    LevelsLevelId = table.Column<int>(type: "int", nullable: true),
                    UsersUser_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessments", x => x.Assessment_ID);
                    table.ForeignKey(
                        name: "FK_Assessments_Topics_TopicsTopic_Id",
                        column: x => x.TopicsTopic_Id,
                        principalTable: "Topics",
                        principalColumn: "Topic_Id");
                    table.ForeignKey(
                        name: "FK_Assessments_Users_UsersUser_ID",
                        column: x => x.UsersUser_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID");
                    table.ForeignKey(
                        name: "FK_Assessments_levels_LevelsLevelId",
                        column: x => x.LevelsLevelId,
                        principalTable: "levels",
                        principalColumn: "LevelId");
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QnInWords = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Option1 = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Option2 = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Option3 = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Option4 = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer = table.Column<int>(type: "int", nullable: false),
                    topicsTopic_Id = table.Column<int>(type: "int", nullable: true),
                    LevelsLevelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QnId);
                    table.ForeignKey(
                        name: "FK_Questions_Topics_topicsTopic_Id",
                        column: x => x.topicsTopic_Id,
                        principalTable: "Topics",
                        principalColumn: "Topic_Id");
                    table.ForeignKey(
                        name: "FK_Questions_levels_LevelsLevelId",
                        column: x => x.LevelsLevelId,
                        principalTable: "levels",
                        principalColumn: "LevelId");
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    result_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalQuestions = table.Column<int>(type: "int", nullable: false),
                    AnsweredQuestions = table.Column<int>(type: "int", nullable: false),
                    UnansweredQuestions = table.Column<int>(type: "int", nullable: false),
                    WrongAnsweredQuestions = table.Column<int>(type: "int", nullable: false),
                    TimeLeft = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    points = table.Column<int>(type: "int", nullable: false),
                    passorfail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    starttime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    endtime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Assessment_ID = table.Column<int>(type: "int", nullable: true),
                    usersUser_ID = table.Column<int>(type: "int", nullable: true),
                    TopicsTopic_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.result_id);
                    table.ForeignKey(
                        name: "FK_Results_Assessments_Assessment_ID",
                        column: x => x.Assessment_ID,
                        principalTable: "Assessments",
                        principalColumn: "Assessment_ID");
                    table.ForeignKey(
                        name: "FK_Results_Topics_TopicsTopic_Id",
                        column: x => x.TopicsTopic_Id,
                        principalTable: "Topics",
                        principalColumn: "Topic_Id");
                    table.ForeignKey(
                        name: "FK_Results_Users_usersUser_ID",
                        column: x => x.usersUser_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID");
                });

            migrationBuilder.CreateTable(
                name: "questionAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    QuestionsQnId = table.Column<int>(type: "int", nullable: true),
                    SelectedOption = table.Column<int>(type: "int", nullable: false),
                    topic_id = table.Column<int>(type: "int", nullable: false),
                    AnswerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_questionAnswers_Questions_QuestionsQnId",
                        column: x => x.QuestionsQnId,
                        principalTable: "Questions",
                        principalColumn: "QnId");
                    table.ForeignKey(
                        name: "FK_questionAnswers_answer_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "answer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_LevelsLevelId",
                table: "Assessments",
                column: "LevelsLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_TopicsTopic_Id",
                table: "Assessments",
                column: "TopicsTopic_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_UsersUser_ID",
                table: "Assessments",
                column: "UsersUser_ID");

            migrationBuilder.CreateIndex(
                name: "IX_levels_TopicsTopic_Id",
                table: "levels",
                column: "TopicsTopic_Id");

            migrationBuilder.CreateIndex(
                name: "IX_questionAnswers_AnswerId",
                table: "questionAnswers",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_questionAnswers_QuestionsQnId",
                table: "questionAnswers",
                column: "QuestionsQnId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_LevelsLevelId",
                table: "Questions",
                column: "LevelsLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_topicsTopic_Id",
                table: "Questions",
                column: "topicsTopic_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Results_Assessment_ID",
                table: "Results",
                column: "Assessment_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Results_TopicsTopic_Id",
                table: "Results",
                column: "TopicsTopic_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Results_usersUser_ID",
                table: "Results",
                column: "usersUser_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "questionAnswers");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "answer");

            migrationBuilder.DropTable(
                name: "Assessments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "levels");

            migrationBuilder.DropTable(
                name: "Topics");
        }
    }
}
